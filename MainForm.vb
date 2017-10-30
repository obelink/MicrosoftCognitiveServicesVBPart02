Imports System.IO
Imports System.Text
Imports Microsoft.ProjectOxford.Vision
Imports Microsoft.ProjectOxford.Vision.Contract

Public Class MainForm

    Private Readonly _visionServiceClient As New VisionServiceClient("17e72298c40a4532901b20c1df72c5b2", 
                                                                     "https://westeurope.api.cognitive.microsoft.com/vision/v1.0")

    Private _facesAnalysisResult As AnalysisResult

    Private Async Function GetAnalysisResultAsync(imageFilePath As String) As Task(Of AnalysisResult)

        Dim visualFeatures() =  {VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, 
                                 VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags}

        Using imageFileStream  = File.OpenRead(imageFilePath)
            Return Await _visionServiceClient.AnalyzeImageAsync(imageFileStream, visualFeatures)
        End Using

    End Function

    Private Async Function GetHandwritingRecognitionOperationResultAsync(imageFilePath As String) As Task(Of HandwritingRecognitionOperationResult)

        Using imageFileStream  = File.OpenRead(imageFilePath)

            Dim operation = Await  _visionServiceClient.CreateHandwritingRecognitionOperationAsync(imageFileStream)
            Dim result = New  HandwritingRecognitionOperationResult() 

            While result.Status = HandwritingRecognitionOperationStatus.Running OrElse 
                  result.Status = HandwritingRecognitionOperationStatus.NotStarted
                result =  Await _visionServiceClient.GetHandwritingRecognitionOperationResultAsync(operation)
                Await Task.Delay(100)
            End While
            
            Return result
        End Using

    End Function

    Private Async Function GetOcrResultsAsync(imageFilePath As String) As Task(Of OcrResults)
        
        Using imageFileStream  = File.OpenRead(imageFilePath)
            Return Await _visionServiceClient.RecognizeTextAsync(imageFileStream, "unk", True)
        End Using
   
    End Function
    
    Private Function GetImageFilePath() As String

        Dim dialog As New OpenFileDialog
        With dialog
            .Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*"
            Return If(.ShowDialog = DialogResult.OK, .FileName, String.Empty)
        End With
  
    End Function

    Private Function ProcessAnalysisResult(result As AnalysisResult) As String

        With result.Description.Captions.FirstOrDefault()
            Return $"{.Text} - {(.Confidence * 100) .ToString("N2")}%"
        End With
        
    End Function

    Private Function ProcessHandwritingRecognitionOperationResult(result As HandwritingRecognitionOperationResult) As String
        
        If result.Status = HandwritingRecognitionOperationStatus.Succeeded Then

            Dim stringBuilder As New StringBuilder

            For Each line In result.RecognitionResult.Lines
                For Each word In line.Words
                    stringBuilder.Append(word.Text).Append(" ")
                Next
                stringBuilder.AppendLine()
            Next

            Return stringBuilder.ToString()
        Else
            Return $"Error; Status: {result.Status}"
        End If

    End Function
    
    Private Function ProcessOcrResults(result As OcrResults) As String

        Dim stringBuilder As New StringBuilder

        For Each ocrRegion In result.Regions
            For Each line In ocrRegion.Lines
                For Each word In line.Words
                    stringBuilder.Append(word.Text).Append(" ")
                Next
                stringBuilder.AppendLine()
            Next
            stringBuilder.AppendLine()
        Next

        MessageBox.Show($"Language: {result.Language}")

        Return stringBuilder.ToString()

    End Function

    Private Async Sub SelectFileButton_Click(sender As Object, e As EventArgs) Handles SelectFileButton.Click

        Dim imageFilePath = GetImageFilePath()

        If imageFilePath <> String.Empty Then
            ImagePictureBox.ImageLocation = imageFilePath

            _facesAnalysisResult = Nothing
            ResultTextBox.Text = "Processing..."

            Select Case VisionTypeComboBox.SelectedIndex
                Case 0  'Analyze image
                    Dim result = Await GetAnalysisResultAsync(imageFilePath)
                    ResultTextBox.Text = ProcessAnalysisResult(result)
                Case 1  'Analyze image (Faces)
                    _facesAnalysisResult = Await GetAnalysisResultAsync(imageFilePath)
                    ResultTextBox.Text = ProcessAnalysisResult(_facesAnalysisResult)
                    ImagePictureBox.Invalidate()
                Case 2  'Recognize text
                    Dim result = Await GetOcrResultsAsync(imageFilePath)
                    ResultTextBox.Text = ProcessOcrResults(result)
                Case 3  'Recognize handwriting
                    Dim result = Await GetHandwritingRecognitionOperationResultAsync(imageFilePath)
                    ResultTextBox.Text = ProcessHandwritingRecognitionOperationResult(result)
            End Select
            
        End If

    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        With VisionTypeComboBox
            .Items.Add("Analyze image")
            .Items.Add("Analyze image (face)")
            .Items.Add("Recognize text")
            .Items.Add("Recognize handwriting")
            .SelectedIndex = 0
        End With

    End Sub

    Private Sub ImagePictureBox_Paint(sender As Object, e As PaintEventArgs) Handles ImagePictureBox.Paint

        If _facesAnalysisResult?.Faces?.Any() Then
            DrawFaces(e)
        End If

    End Sub

    Private Sub DrawFaces(e As PaintEventArgs)
      
        ' Calculate ScaleFactor
        Dim scaleFactorX = ImagePictureBox.Width / ImagePictureBox.Image.Width
        Dim scaleFactorY = ImagePictureBox.Height / ImagePictureBox.Image.Height

        For Each face In _facesAnalysisResult.Faces
            DrawFace(e, scaleFactorX, scaleFactorY, face)
        Next

    End Sub
    
    Private Sub DrawFace(e As PaintEventArgs, scaleFactorX As Double, scaleFactorY As Double, face As Face)

        Dim rectangle = New Drawing.Rectangle(face.FaceRectangle.Left * scaleFactorX, 
                                              face.FaceRectangle.Top * scaleFactorY, 
                                              face.FaceRectangle.Width * scaleFactorX, 
                                              face.FaceRectangle.Height * scaleFactorY)

        Using pen = New Pen(Drawing.Color.Red, 4)
            e.Graphics.DrawRectangle(pen, rectangle)
        End Using

        Dim caption = $"{face.Age} - {face.Gender}"
        Dim yellowBrush = New SolidBrush(Drawing.Color.Yellow)
        e.Graphics.DrawString(caption, New Font("Arial", 12), yellowBrush, rectangle)
    End Sub

End Class
