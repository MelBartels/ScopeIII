Public Class InitStateFactory

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As InitStateFactory
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As InitStateFactory = New InitStateFactory
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As InitStateFactory
        Return New InitStateFactory
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Build InitStateTemplate based on final strong types.
    ''' </summary>
    ''' <param name="CoordXformType"></param>
    ''' <param name="InitStateType"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	12/21/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function Build(ByRef CoordXformType As ISFT, ByRef InitStateType As ISFT) As InitStateTemplate
        Dim InitStateTemplate As InitStateTemplate = Coordinates.InitStateTemplate.GetInstance

        InitStateTemplate.CoordXformType = CoordXformType
        InitStateTemplate.ICoordXform = CoordXformFactory.GetInstance.Build(CoordXformType)

        SetInit(InitStateTemplate, InitStateType)

        Return InitStateTemplate
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''  set InitStateTemplate's IInit based on InitStateType, eg, InitStateType.Equatorial, InitStateType.Altazimuth
    ''' </summary>
    ''' <param name="InitStateTemplate"></param>
    ''' <param name="InitStateType"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	12/21/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub SetInit(ByRef InitStateTemplate As InitStateTemplate, ByRef InitStateType As ISFT)
        InitStateTemplate.InitStateType = InitStateType

        If InitStateType Is Nothing OrElse InitStateType Is ScopeIII.Coordinates.InitStateType.None Then
            InitStateTemplate.IInit = Nothing

        ElseIf InitStateTemplate.CoordXformType Is CoordXformType.ConvertTrig Then
            InitStateTemplate.IInit = InitFactory.GetInstance.Build(CType(initType.InitDoNothing, ISFT), InitStateTemplate.ICoordXform)

        Else
            Dim eInitType As IEnumerator = initType.ISFT.Enumerator
            While eInitType.MoveNext
                Dim initType As ISFT = CType(eInitType.Current, ISFT)
                ' eg, is the string 'Equatorial' from InitType.InitConvertMatrixEquatorial found in InitStateType.Equatorial ?
                If initType.Name.IndexOf(InitStateType.Name) > -1 Then
                    InitStateTemplate.IInit = InitFactory.GetInstance.Build(initType, InitStateTemplate.ICoordXform)
                    Exit While
                End If
            End While
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class