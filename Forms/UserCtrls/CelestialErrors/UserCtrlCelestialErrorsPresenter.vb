#Region "Imports"
#End Region

Public Class UserCtrlCelestialErrorsPresenter
    Inherits MVPUserCtrlPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public IncludePrecession As Boolean
    Public IncludeNutationAnnualAberration As Boolean
    Public IncludeRefraction As Boolean
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlCelestialErrors As UserCtrlCelestialErrors

    Dim al As ArrayList
    Dim RACoordExp As ICoordExp
    Dim DecCoordExp As ICoordExp
    Dim AzCoordExp As ICoordExp
    Dim AltCoordExp As ICoordExp
    Dim position As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlCelestialErrorsPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlCelestialErrorsPresenter = New UserCtrlCelestialErrorsPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlCelestialErrorsPresenter
        Return New UserCtrlCelestialErrorsPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Update()
        loadViewFromModel()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlCelestialErrors = CType(IMVPUserCtrl, UserCtrlCelestialErrors)

        al = New ArrayList
        RACoordExp = FormattedHMSM.GetInstance
        DecCoordExp = FormattedDMS.GetInstance
        AzCoordExp = FormattedDegree.GetInstance
        AltCoordExp = FormattedDegree.GetInstance
    End Sub

    Protected Overrides Sub loadViewFromModel()
        pUserCtrlCelestialErrors.CelestialErrorsDataSource = adaptDataModel()
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function adaptDataModel() As ArrayList
        al.Clear()
        position = CType(DataModel, Position)

        ' build hashtable where key is CoordErrorType, and value is an array of CoordError;
        ' this consolidates the CoordErrorArray by CoordErrorType, adding the CoordName to the value'
        ' # of CoordErrors should remain constant otherwise DataGridView data errors
        'Debug.WriteLine("# of CoordErrors " & position.CoordErrorArray.ErrorArray.Count)

        Dim ht As New Hashtable
        For Each coordError As CoordError In position.CoordErrorArray.ErrorArray
            Dim key As Object = coordError.CoordErrorType
            Dim valuesArray() As Object
            If ht.Contains(key) Then
                valuesArray = CType(ht.Item(key), Object())
                ReDim Preserve valuesArray(valuesArray.Length)
                valuesArray(valuesArray.Length - 1) = coordError
                ht.Item(key) = valuesArray
            Else
                valuesArray = New Object() {coordError}
                ht.Add(key, valuesArray)
            End If
        Next

        ' build array of DisplayPosition, where each DisplayPosition represents a CoordErrorType 
        ' and all its coordinates
        Dim eht As IEnumerator = ht.GetEnumerator
        While eht.MoveNext
            Dim de As DictionaryEntry = CType(eht.Current, DictionaryEntry)
            Dim key As ISFT = CType(de.Key, ISFT)
            Dim displayPosition As DisplayPosition = Coordinates.DisplayPosition.GetInstance
            displayPosition.Name = key.Name
            Dim valuesArray() As Object = CType(de.Value, [Object]())
            For Each coordError As CoordError In valuesArray
                If coordError.CoordName Is CoordName.RA Then
                    displayPosition.RA = coordError.Coordinate.Rad
                    displayPosition.RADisplay = RACoordExp.ToString(coordError.Coordinate.Rad)
                ElseIf coordError.CoordName Is CoordName.Dec Then
                    displayPosition.Dec = coordError.Coordinate.Rad
                    displayPosition.DecDisplay = DecCoordExp.ToString(coordError.Coordinate.Rad)
                ElseIf coordError.CoordName Is CoordName.Az Then
                    displayPosition.Az = coordError.Coordinate.Rad
                    displayPosition.AzDisplay = AzCoordExp.ToString(coordError.Coordinate.Rad)
                ElseIf coordError.CoordName Is CoordName.Alt Then
                    displayPosition.Alt = coordError.Coordinate.Rad
                    displayPosition.AltDisplay = AltCoordExp.ToString(coordError.Coordinate.Rad)
                End If
            Next
            al.Add(displayPosition)
        End While

        al.Sort(CelestialErrorComparer.GetInstance)

        ' include starting position values with an empty row as a spacer
        Dim emptyRow As DisplayPosition = DisplayPosition.GetInstance
        al.Insert(0, emptyRow)
        Dim preDisplayPosition As DisplayPosition = DisplayPosition.GetInstance
        preDisplayPosition.Name = "Starting Values"
        preDisplayPosition.RADisplay = RACoordExp.ToString(position.RA.Rad)
        preDisplayPosition.DecDisplay = DecCoordExp.ToString(position.Dec.Rad)
        al.Insert(0, preDisplayPosition)

        ' end with sum of correction values, a spacer row, and finally the corrected position values 
        Dim sumDisplayPosition As DisplayPosition = DisplayPosition.GetInstance
        sumDisplayPosition.Name = "Total Correction"
        Dim totalRaCorrectionRad As Double = Me.totalRaCorrectionRad(position)
        Dim totalDecCorrectionRad As Double = Me.totalDecCorrectionRad(position)
        sumDisplayPosition.RADisplay = RACoordExp.ToString(totalRaCorrectionRad)
        sumDisplayPosition.DecDisplay = DecCoordExp.ToString(totalDecCorrectionRad)
        al.Add(sumDisplayPosition)

        al.Add(emptyRow)
        Dim postDisplayPosition As DisplayPosition = DisplayPosition.GetInstance
        postDisplayPosition.Name = "Corrected Values"
        postDisplayPosition.RADisplay = RACoordExp.ToString(eMath.ValidRad(position.RA.Rad + totalRaCorrectionRad))
        postDisplayPosition.DecDisplay = DecCoordExp.ToString(position.Dec.Rad + totalDecCorrectionRad)
        al.Add(postDisplayPosition)

        Return al
    End Function

    Private Function totalRaCorrectionRad(ByRef position As Position) As Double
        Return totalCorrectionRad(position, CType(CoordName.RA, ISFT))
    End Function

    Private Function totalDecCorrectionRad(ByRef position As Position) As Double
        Return totalCorrectionRad(position, CType(CoordName.Dec, ISFT))
    End Function

    Private Function totalCorrectionRad(ByRef position As Position, ByRef coordName As ISFT) As Double
        Dim total As Double = 0

        If IncludePrecession Then
            total += position.CoordErrorArray.CoordError(coordName, CType(CoordErrorType.Precession, ISFT)).Rad
        End If

        If IncludeNutationAnnualAberration Then
            total += position.CoordErrorArray.CoordError(coordName, CType(CoordErrorType.Nutation, ISFT)).Rad
            total += position.CoordErrorArray.CoordError(coordName, CType(CoordErrorType.AnnualAberration, ISFT)).Rad
        End If

        If IncludeRefraction Then
            total += position.CoordErrorArray.CoordError(coordName, CType(CoordErrorType.Refraction, ISFT)).Rad
        End If

        Return total
    End Function
#End Region

End Class
