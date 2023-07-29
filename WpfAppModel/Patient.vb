Public Class Patient

    Public _PatientCode As Int32
    Property PatientCode As Int32
        Get
            Return _PatientCode
        End Get
        Set(value As Int32)
            _PatientCode = value
        End Set
    End Property

    Public _PatientName As String
    Property PatientName As String
        Get
            Return _PatientName
        End Get
        Set(value As String)
            _PatientName = value
        End Set
    End Property

    Public _Disease As String
    Property Disease As String
        Get
            Return _Disease
        End Get
        Set(value As String)
            _Disease = value
        End Set
    End Property

    Public _Address As String
    Property Address As String
        Get
            Return _Address
        End Get
        Set(value As String)
            _Address = value
        End Set
    End Property

    Public _City As String
    Property City As String
        Get
            Return _City
        End Get
        Set(value As String)
            _City = value
        End Set
    End Property

    Public _Mobile As String
    Property Mobile As String
        Get
            Return _Mobile
        End Get
        Set(value As String)
            _Mobile = value
        End Set
    End Property

End Class
