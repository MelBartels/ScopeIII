Imports System.IO

Public Class UserCtrlSitePresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlSite As UserCtrlSite

    Private pUserCtrlCoordPresenterLatitude As UserCtrlCoordPresenter
    Private pUserCtrlCoordPresenterLongitude As UserCtrlCoordPresenter

    Private pSite As Coordinates.Site
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlSitePresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlSitePresenter = New UserCtrlSitePresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlSitePresenter
        Return New UserCtrlSitePresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Site() As Coordinates.Site
        Get
            Return pSite
        End Get
        Set(ByVal Value As Coordinates.Site)
            pSite = Value
        End Set
    End Property

    Public Sub SetLatitudeLongitude(ByVal latitudeRad As Double, ByVal longitudeRad As Double)
        pUserCtrlCoordPresenterLatitude.DisplayCoordinate(latitudeRad)
        pUserCtrlCoordPresenterLongitude.DisplayCoordinate(longitudeRad)
        Site.Latitude = pUserCtrlCoordPresenterLatitude.Coordinate
        Site.Longitude = pUserCtrlCoordPresenterLongitude.Coordinate
    End Sub

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Select Case CType([object], Coordinate).Name
            Case CoordName.Latitude.Description, _
                 CoordName.Longitude.Description

                Site.Latitude = pUserCtrlCoordPresenterLatitude.Coordinate
                Site.Longitude = pUserCtrlCoordPresenterLongitude.Coordinate
                Return True
        End Select

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlSite = CType(IMVPUserCtrl, UserCtrlSite)
        AddHandler pUserCtrlSite.NewSiteName, AddressOf newSiteName

        Site = Coordinates.Site.GetInstance

        pUserCtrlCoordPresenterLatitude = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterLatitude.IMVPUserCtrl = CType(pUserCtrlSite.UserCtrlCoordLatitude, UserCtrlCoord)
        pUserCtrlCoordPresenterLatitude.SetCoordinateName(CoordName.Latitude.Description)
        pUserCtrlCoordPresenterLatitude.CoordinateObservableImp.Attach(Me)

        pUserCtrlCoordPresenterLongitude = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterLongitude.IMVPUserCtrl = CType(pUserCtrlSite.UserCtrlCoordLongitude, UserCtrlCoord)
        pUserCtrlCoordPresenterLongitude.SetCoordinateName(CoordName.Longitude.Description)
        pUserCtrlCoordPresenterLongitude.CoordinateObservableImp.Attach(Me)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub newSiteName(ByVal name As String)
        Site.Name = name
    End Sub

#End Region

End Class
