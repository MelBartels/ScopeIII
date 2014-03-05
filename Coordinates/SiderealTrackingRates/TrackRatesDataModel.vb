#Region "Imports"
#End Region

Public Class TrackRatesDataModel

#Region "Inner Classes"
    Public Class TrackRate
        Private pRateRadPerSidSec As Double
        Private pCorrectedRateRadPerSidSec As Double
        Private pDeltaRateRadPerSidSecPerSidSec As Double

        Public Property RateRadPerSidSec() As Double
            Get
                Return pRateRadPerSidSec
            End Get
            Set(ByVal value As Double)
                pRateRadPerSidSec = eMath.ValidRadPi(value)
            End Set
        End Property
        Public Property CorrectedRateRadPerSidSec() As Double
            Get
                Return pCorrectedRateRadPerSidSec
            End Get
            Set(ByVal value As Double)
                pCorrectedRateRadPerSidSec = eMath.ValidRadPi(value)
            End Set
        End Property
        Public Property DeltaRateRadPerSidSecPerSidSec() As Double
            Get
                Return pDeltaRateRadPerSidSecPerSidSec
            End Get
            Set(ByVal value As Double)
                pDeltaRateRadPerSidSecPerSidSec = eMath.ValidRadPi(value)
            End Set
        End Property
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pHT As Hashtable
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TrackRatesDataModel
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TrackRatesDataModel = New TrackRatesDataModel
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        buildHashTable()
    End Sub

    Public Shared Function GetInstance() As TrackRatesDataModel
        Return New TrackRatesDataModel
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function GetTrackRate(ByRef coordname As ISFT) As TrackRate
        Return CType(pHT.Item(coordname), TrackRate)
    End Function

    Public Sub SetTrackRate(ByRef coordname As ISFT, ByVal rate As Double)
        CType(pHT.Item(coordname), TrackRate).RateRadPerSidSec = rate
    End Sub

    Public Function GetPriAxisTrackRate() As TrackRate
        Return GetTrackRate(CType(CoordName.PriAxis, ISFT))
    End Function

    Public Function GetSecAxisTrackRate() As TrackRate
        Return GetTrackRate(CType(CoordName.SecAxis, ISFT))
    End Function

    Public Function GetTierAxisTrackRate() As TrackRate
        Return GetTrackRate(CType(CoordName.TierAxis, ISFT))
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub buildHashTable()
        pHT = New Hashtable
        pHT.Add(CoordName.PriAxis, New TrackRate)
        pHT.Add(CoordName.SecAxis, New TrackRate)
        pHT.Add(CoordName.TierAxis, New TrackRate)
    End Sub
#End Region

End Class
