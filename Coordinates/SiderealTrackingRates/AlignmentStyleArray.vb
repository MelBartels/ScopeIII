Public Class AlignmentStyleArray

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pArray As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As AlignmentStyleArray
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AlignmentStyleArray = New AlignmentStyleArray
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AlignmentStyleArray
        Return New AlignmentStyleArray
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Array() As ArrayList
        Get
            Return pArray
        End Get
        Set(ByVal Value As ArrayList)
            pArray = Value
        End Set
    End Property

    Public Function BuildArray(ByRef rate As ISFT) As ArrayList
        pArray = New ArrayList

        pArray.Add(AlignmentStyle.PolarAligned)
        pArray.Add(AlignmentStyle.AltazSiteAligned)

        If rate Is Rates.MatrixRates Then
            pArray.Add(AlignmentStyle.CelestialAligned)
        End If

        Return pArray
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
