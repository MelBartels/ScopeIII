Public Class AnalysisErrors

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ICoordXform As ICoordXform
    Public PositionArray As PositionArray
    Public PointingErrorRMS As Double
    Public MaxPointingErrorRMS As Double
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As AnalysisErrors
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AnalysisErrors = New AnalysisErrors
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AnalysisErrors
        Return New AnalysisErrors
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Calc() As Boolean
        Dim pointingErrorRMSTotal As Double
        Dim tempPosition As position = PositionArraySingleton.GetInstance.GetPosition
        tempPosition.CopyFrom(ICoordXform.Position)
        PointingErrorRMS = 0
        MaxPointingErrorRMS = 0

        For Each position As Position In PositionArray.PositionArray
            ICoordXform.Position.CopyFrom(position)
            ICoordXform.GetAltaz()
            'DebugTrace.Writeline(CType(ICoordXform, ConvertMatrix).One.ShowCoordDeg)
            'DebugTrace.Writeline(CType(ICoordXform, ConvertMatrix).Two.ShowCoordDeg)
            'DebugTrace.Writeline(ICoordXform.Position.ShowCoordDeg)
            'DebugTrace.Writeline(CType(PositionArray.PositionArray.Item(ix), position).ShowCoordDeg)

            Dim AError As Coordinate = ICoordXform.Position.CoordErrorArray.CoordError(CType(CoordName.Alt, ISFT), CType(CoordErrorType.CoordXform, ISFT))
            Dim ZError As Coordinate = ICoordXform.Position.CoordErrorArray.CoordError(CType(CoordName.Az, ISFT), CType(CoordErrorType.CoordXform, ISFT))

            ' if scope aimed higher or more CW than what it should be, then it is defined as a positive error
            AError.Rad = position.Alt.Rad - ICoordXform.Position.Alt.Rad
            ' azimuth errors in terms of true field decrease towards the zenith
            ZError.Rad = (position.Az.Rad - ICoordXform.Position.Az.Rad) * Math.Cos(position.Alt.Rad)
            PointingErrorRMS = Math.Sqrt(AError.Rad * AError.Rad + ZError.Rad * ZError.Rad)
            pointingErrorRMSTotal += PointingErrorRMS
            If PointingErrorRMS > MaxPointingErrorRMS Then
                MaxPointingErrorRMS = PointingErrorRMS
            End If
            'DebugTrace.Writeline(AError.Rad * Units.RadToArcsec & " " & ZError.Rad * Units.RadToArcsec & " " & PointingErrorRMS * Units.RadToArcsec)
        Next

        PointingErrorRMS = pointingErrorRMSTotal / PositionArray.PositionArray.Count
        ICoordXform.Position.CopyFrom(tempPosition)
        tempPosition.Available = True

        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
