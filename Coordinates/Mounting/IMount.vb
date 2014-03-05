Public Interface IMount
    Property MountType() As ISFT
    Property PriAxisAlign() As ISFT
    Property CanMoveToPriAxisPole() As Boolean
    Property CanMoveThruPriAxisPole() As Boolean
    Property PriAxisFullyRotates() As Boolean
    Property MeridianFlip() As MeridianFlip
End Interface
