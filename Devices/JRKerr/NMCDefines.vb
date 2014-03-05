' Mel Bartels' changes to make file compatible with Visual Studio 2008
' change  '= &H' to 'as byte = &H'
' change remaining 'missing 'As' clause' errors to bytes

Module NMCDefines

    '
    'This module contains constant and function definitions used
    'in NMCLIB04.DLL.  There are four sets of constants and function
    'declarations for the following areas of operations:
    '       Core NMC module communications
    '       PIC-SERVO operations
    '       PIC-I/O operations
    '       PIC-STEP operations
    '

    '
    '****** Core NMC Communications definitions ******
    '

    '
    'Module type definitions:
    '
    Public Const SERVOMODTYPE As Byte = 0
    Public Const IOMODTYPE As Byte = 2
    Public Const STEPMODTYPE As Byte = 3

    '
    'NMC Communications functions
    '
    Declare Function NmcInit Lib "nmclib04" (ByVal portname As String, ByVal baudrate As Integer) As Integer
    Declare Function NmcGetModType Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Sub NmcShutdown Lib "nmclib04" ()
    Declare Function NmcDefineStatus Lib "nmclib04" (ByVal addr As Byte, ByVal statusitems As Byte) As Boolean
    Declare Sub InitVars Lib "nmclib04" ()
    Declare Function NmcSendCmd Lib "nmclib04" (ByVal addr As Byte, ByVal cmd As Byte, ByVal datastr As String, ByVal n As Byte, ByVal stataddr As Byte) As Boolean
    Declare Sub FixSioError Lib "nmclib04" (ByVal addr As Byte)
    Declare Function NmcSetGroupAddr Lib "nmclib04" (ByVal addr As Byte, ByVal groupaddr As Byte, ByVal leader As Boolean) As Boolean
    Declare Function NmcReadStatus Lib "nmclib04" (ByVal addr As Byte, ByVal statusitems As Byte) As Boolean
    Declare Function NmcSynchOutput Lib "nmclib04" (ByVal groupaddr As Byte, ByVal leaderaddr As Byte) As Boolean
    Declare Function NmcChangeBaud Lib "nmclib04" (ByVal groupaddr As Byte, ByVal baudrate As Integer) As Boolean
    Declare Function NmcSynchInput Lib "nmclib04" (ByVal groupaddr As Byte, ByVal leaderaddr As Byte) As Boolean
    Declare Function NmcNoOp Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function NmcHardReset Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function NmcGetStat Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function NmcGetModVer Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function NmcGetGroupAddr Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function NmcGroupLeader Lib "nmclib04" (ByVal addr As Byte) As Byte



    '
    '****** Servo Module Definitions ******
    '

    '
    'Servo Module Command set:
    '
    Public Const RESET_POS As Byte = &H0        'Reset encoder counter to 0 (0 bytes)
    Public Const SET_ADDR As Byte = &H1         'Set address and group address (2 bytes)
    Public Const DEF_STAT As Byte = &H2         'Define status items to return (1 byte)
    Public Const READ_STAT As Byte = &H3        'Read value of current status items
    Public Const LOAD_TRAJ As Byte = &H4        'Load trahectory date (1 - 14 bytes)
    Public Const START_MOVE As Byte = &H5       'Start pre-loaded trajectory (0 bytes)
    Public Const SET_GAIN As Byte = &H6         'Set servo gain and control parameters (13 or 14)
    Public Const STOP_MOTOR As Byte = &H7       'Stop motor (1 byte)
    Public Const IO_CTRL As Byte = &H8          'Define bit directions and set output (1 byte)
    Public Const SET_HOMING As Byte = &H9       'Define homing mode (1 byte)
    Public Const SET_BAUD As Byte = &HA         'Set the baud rate (1 byte)
    Public Const CLEAR_BITS As Byte = &HB       'Save current pos. in home pos. register (0 bytes)
    Public Const SAVE_AS_HOME As Byte = &HC     'Store the input bytes and timer val (0 bytes)
    Public Const ADD_PATHPOINT As Byte = &HD    'Adds path points for path mode
    Public Const NOP As Byte = &HE              'No operation - returns prev. defined status (0 bytes)
    Public Const HARD_RESET As Byte = &HF       'RESET - no status is returned

    '
    'Servo Module STATUSITEMS bit definitions:
    '
    Public Const SEND_POS As Byte = &H1         '4 bytes data
    Public Const SEND_AD As Byte = &H2          '1 byte
    Public Const SEND_VEL As Byte = &H4         '2 bytes
    Public Const SEND_AUX As Byte = &H8         '1 byte
    Public Const SEND_HOME As Byte = &H10       '4 bytes
    Public Const SEND_ID As Byte = &H20         '2 bytes
    Public Const SEND_PERROR As Byte = &H40     '2 bytes
    Public Const SEND_NPOINTS As Byte = &H80    '1 byte

    '
    'Servo Module STOP_MOTOR control byte bit definitions:
    '
    Public Const AMP_ENABLE As Byte = &H1       '1 = raise amp enable output, 0 = lower amp enable
    Public Const MOTOR_OFF As Byte = &H2        ' set to turn motor off
    Public Const STOP_ABRUPT As Byte = &H4      ' set to stop motor immediately
    Public Const STOP_SMOOTH As Byte = &H8      ' set to decellerate motor smoothly
    Public Const STOP_HERE As Byte = &H10       ' set to stop at position (4 add'l data bytes required)
    Public Const ADV_FEATURE As Byte = &H20     ' enable features in ver. CMC

    '
    'Servo Module LOAD_TRAJ control byte bit definitions:
    '
    Public Const LOAD_POS As Byte = &H1         ' +4 bytes
    Public Const LOAD_VEL As Byte = &H2         ' +4 bytes
    Public Const LOAD_ACC As Byte = &H4         ' +4 bytes
    Public Const LOAD_PWM As Byte = &H8         ' +1 byte
    Public Const ENABLE_SERVO As Byte = &H10    ' 1 = servo mode, 0 = PWM mode
    Public Const VEL_MODE As Byte = &H20        ' 1 = velocity mode, 0 = trap. position mode
    Public Const REVERSE As Byte = &H40         ' 1 = command neg. PWM or vel, 0 = positive
    Public Const MOVE_REL As Byte = &H40        ' 1 = move relative, 0 = move absolute
    Public Const START_NOW As Byte = &H80       ' 1 = start now, 0 = wait for START_MOVE command

    '
    'Servo Module RESET_POS control byte bit definitions:
    '
    Public Const REL_HOME As Byte = &H1         ' Reset position relative to current home position
    Public Const SET_POS As Byte = &H2          ' Set the position to a specific value (v10 & >)

    '
    'Servo Module IO_CTRL control byte bit definitions:
    '
    Public Const SET_OUT1 As Byte = &H1         ' 1 = set limit 1 output, 0 = clear limit 1 output
    Public Const SET_OUT2 As Byte = &H2         ' 1 = set limit 2 output, 0 = clear limit 2 output
    Public Const IO1_IN As Byte = &H4           ' 1 = limit 1 is an input, 0 = limit 1 is an output
    Public Const IO2_IN As Byte = &H8           ' 1 = limit 2 is an input, 0 = limit 2 is an output
    Public Const LIMSTOP_OFF As Byte = &H4     ' turn off motor on limit
    Public Const LIMSTOP_ABRUPT As Byte = &H8  ' stop abruptly on limit
    Public Const THREE_PHASE As Byte = &H10     ' 1 = 3-phase mode, 0 = single PWM channel
    Public Const ANTIPHASE As Byte = &H20       ' 1 = antiphase (0 = 50% duty cycle), 0 = PWM & dir
    Public Const FAST_PATH As Byte = &H40       ' 0 = 30 or 60 Hz path execution, 1 = 60 or 120 Hz
    Public Const STEP_MODE As Byte = &H80       ' 0 = 30 or 60 Hz path execution, 1 = 60 or 120 Hz

    '
    'Servo Module SET_HOMING control byte bit definitions:
    '
    Public Const ON_LIMIT1 As Byte = &H1        ' home on change in limit 1
    Public Const ON_LIMIT2 As Byte = &H2        ' home on change in limit 2
    Public Const HOME_MOTOR_OFF As Byte = &H4   ' turn motor off when homed
    Public Const ON_INDEX As Byte = &H8           ' home on change in index
    Public Const HOME_STOP_ABRUPT As Byte = &H10  ' stop abruptly when homed
    Public Const HOME_STOP_SMOOTH As Byte = &H20  ' stop smoothly when homed
    Public Const ON_POS_ERR As Byte = &H40      ' home on excessive position error
    Public Const ON_CUR_ERR As Byte = &H80      ' home on overcurrent error

    '
    'Servo Module ADD_PATHPOINT frequency definitions
    '
    Public Const P_30HZ As Byte = 30        '30 hz path resolution
    Public Const P_60HZ As Byte = 60        '60 hs path resolution
    Public Const P_120HZ As Byte = 120      '120 hs path resolution

    '
    'Servo Module HARD_RESET control byte bit definitions (v.10 and higher only):
    '
    Public Const SAVE_DATA As Byte = &H1        ' save config. data in EPROM
    Public Const RESTORE_ADDR As Byte = &H2     ' restore addresses on power-up
    Public Const EPU_AMP As Byte = &H4          ' enable amplifier on power-up
    Public Const EPU_SERVO As Byte = &H8        ' enable servo
    Public Const EPU_STEP As Byte = &H10        ' enable step & direction mode
    Public Const EPU_LIMITS As Byte = &H20      ' enable limit switch protection
    Public Const EPU_3PH As Byte = &H40         ' enable 3-phase commutation
    Public Const EPU_ANTIPHASE As Byte = &H80   ' enable antiphase PWM

    '
    'Servo Module Status byte bit definitions:
    '
    Public Const MOVE_DONE As Byte = &H1        ' set when move done (trap. pos mode), when goal
    '    vel. has been reached (vel mode) or when not
    '    servoing
    Public Const CKSUM_ERROR As Byte = &H2      ' checksum error in received command
    Public Const OVERCURRENT As Byte = &H4      ' set on overcurrent condition (sticky bit)
    Public Const POWER_ON As Byte = &H8         ' set when motor power is on
    Public Const POS_ERR As Byte = &H10         ' set on excess pos. error (sticky bit)
    Public Const LIMIT1 As Byte = &H20          ' value of limit 1 input
    Public Const LIMIT2 As Byte = &H40          ' value of limit 2 input
    Public Const HOME_IN_PROG As Byte = &H80    ' set while searching for home, cleared when home found

    '
    'Servo Module Auxilliary status byte bit definitions:
    '
    Public Const INDEX As Byte = &H1            ' value of the encoder index signal
    Public Const POS_WRAP As Byte = &H2         ' set when 32 bit position counter wraps around
    '   (sticky bit)
    Public Const SERVO_ON As Byte = &H4         ' set when position servo is operating
    Public Const ACCEL_DONE As Byte = &H8       ' set when acceleration portion of a move is done
    Public Const SLEW_DONE As Byte = &H10       ' set when slew portion of a move is done
    Public Const SERVO_OVERRUN As Byte = &H20   ' set if servo takes longer than the specified
    '    servo period to execute
    Public Const PATH_MODE As Byte = &H40       ' path mode is enabled (v.5)

    '
    'Servo Module Function declarations
    '
    Declare Sub ServoNewMod Lib "nmclib04" (ByVal addr As Byte)
    Declare Function ServoGetStat Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function ServoGetAD Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetVel Lib "nmclib04" (ByVal addr As Byte) As Short
    Declare Function ServoGetAux Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetHome Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function ServoGetCmdPos Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function ServoGetCmdVel Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function ServoGetCmdAcc Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function ServoGetStopPos Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function ServoGetCmdPwm Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetMoveCtrl Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetStopCtrl Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetHomeCtrl Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetIoCtrl Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetPhAdv Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function ServoGetPhOff Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Sub ServoGetGain Lib "nmclib04" (ByVal addr As Byte, ByVal kp As Short, ByVal kd As Short, ByVal ki As Short, ByVal il As Short, ByVal ol As Byte, ByVal cl As Byte, ByVal el As Short, ByVal sr As Byte, ByVal dc As Byte)
    Declare Function ServoSetGain Lib "nmclib04" (ByVal addr As Byte, ByVal kp As Short, ByVal kd As Short, ByVal ki As Short, ByVal il As Short, ByVal ol As Byte, ByVal cl As Byte, ByVal el As Short, ByVal sr As Byte, ByVal dc As Byte) As Boolean
    Declare Function ServoSetGain2 Lib "nmclib04" (ByVal addr As Byte, ByVal kp As Short, ByVal kd As Short, ByVal ki As Short, ByVal il As Short, ByVal ol As Byte, ByVal cl As Byte, ByVal el As Short, ByVal sr As Byte, ByVal dc As Byte, ByVal sm As Byte) As Boolean
    Declare Function ServoResetPos Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function ServoSetPos Lib "nmclib04" (ByVal addr As Byte, ByVal pos As Integer) As Boolean
    Declare Function ServoStopHere Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte, ByVal pos As Integer) As Boolean
    Declare Function ServoClearBits Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function ServoStopMotor Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte) As Boolean
    Declare Function ServoSetHoming Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte) As Boolean
    Declare Function ServoLoadTraj Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte, ByVal pos As Integer, ByVal vel As Integer, ByVal acc As Integer, ByVal pwm As Byte) As Boolean
    Declare Function ServoGetPos Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function ServoSetPhase Lib "nmclib04" (ByVal addr As Byte, ByVal padvance As Integer, ByVal poffset As Integer) As Boolean
    Declare Function ServoHardReset Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte) As Boolean

    '
    '****** I/O Module Definitions ******
    '(note: some defined constants which are repeats of constants for the 
    ' PIC-SERVO are shown for your reference, but are commented out)
    '

    '
    'IO Module Command set:
    '
    Public Const SET_IO_DIR As Byte = &H0       ' Set direction of IO bits (2 data bytes)
    'Public Const SET_ADDR as byte = &H1        'Set address and group address (2 bytes)
    'Public Const DEF_STAT as byte = &H2        'Define status items to return (1 byte)
    Public Const SET_PWM As Byte = &H4          ' Immediatley set PWM1 and PWM2 (2 bytes)
    Public Const SYNCH_OUT As Byte = &H5        ' Output prev. stored PWM & output bytes (0 bytes)
    Public Const SET_OUTPUT As Byte = &H6       ' Immediately set output bytes
    Public Const SET_SYNCH_OUT As Byte = &H7    ' Store PWM & outputs for synch'd output (4 bytes)
    Public Const SET_TMR_MODE As Byte = &H8     ' Set the counter/timer mode (1 byte)
    'Public Const SET_HOMING as byte = &H9      'Define homing mode (1 byte)
    'Public Const SET_BAUD as byte = &HA        'Set the baud rate (1 byte)
    Public Const SYNCH_INPUT As Byte = &HC      ' Store the input bytes and timer val (0 bytes)
    'Public Const NOP as byte = &HE             'No operation - returns prev. defined status (0 bytes)
    'Public Const HARD_RESET as byte = &HF      'RESET - no status is returned

    '
    'IO Module STATUSITEMS bit definitions
    '
    Public Const SEND_INPUTS As Byte = &H1      ' 2 bytes data
    Public Const SEND_AD1 As Byte = &H2         ' 1 byte
    Public Const SEND_AD2 As Byte = &H4         ' 1 byte
    Public Const SEND_AD3 As Byte = &H8         ' 1 byte
    Public Const SEND_TIMER As Byte = &H10      ' 4 bytes
    'Public Const SEND_ID as byte = &H20        ' 2 bytes
    Public Const SEND_SYNC_IN As Byte = &H40    ' 2 bytes
    Public Const SEND_SYNC_TMR As Byte = &H80   ' 4 bytes

    '
    'IO Module Timer mode definitions
    'Timer mode and resolution may be OR'd together
    '
    Public Const OFFMODE As Byte = &H0
    Public Const COUNTERMODE As Byte = &H3
    Public Const TIMERMODE As Byte = &H1
    Public Const RESx1 As Byte = &H0
    Public Const RESx2 As Byte = &H10
    Public Const RESx4 As Byte = &H20
    Public Const RESx8 As Byte = &H30

    '
    ' IO Module specific stuff
    '
    Declare Sub IoNewMod Lib "nmclib04" ()
    Declare Function IoGetStat Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function IoInBitVal Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Byte) As Boolean
    Declare Function IoInBitSVal Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Byte) As Boolean
    Declare Function IoOutBitVal Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Byte) As Boolean
    Declare Function IoGetBitDir Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Byte) As Boolean
    Declare Function IoGetADCVal Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Byte) As Byte
    Declare Function IoGetPWMVal Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Byte) As Byte
    Declare Function IoGetTimerVal Lib "nmclib04" (ByVal addr As Byte) As Int32
    Declare Function IoGetTimerSVal Lib "nmclib04" (ByVal addr As Byte) As Int32
    Declare Function IoGetTimerMode Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function IoSetOutBit Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Integer) As Boolean
    Declare Function IoClrOutBit Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Integer) As Boolean
    Declare Function IoBitDirIn Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Integer) As Boolean
    Declare Function IoBitDirOut Lib "nmclib04" (ByVal addr As Byte, ByVal bitnum As Integer) As Boolean
    Declare Function IoSetPWMVal Lib "nmclib04" (ByVal addr As Byte, ByVal pwm1 As Byte, ByVal pwm2 As Byte) As Boolean
    Declare Function IoSetTimerMode Lib "nmclib04" (ByVal addr As Byte, ByVal pwm1 As Byte) As Boolean
    Declare Function IoSetSynchOutput Lib "nmclib04" (ByVal addr As Byte, ByVal outbits As Integer, ByVal pwm1 As Byte, ByVal pwm2 As Byte) As Boolean


    '
    '****** Stepper Module Definitions ******
    '(note: some defined constants which are repeats of constants for the 
    ' PIC-SERVO are shown for your reference, but are commented out)
    '

    '
    'Step Module Command set:
    '
    'Public Const RESET_POS as byte = &H0     'Reset encoder counter to 0 (0 bytes)
    'Public Const SET_ADDR as byte = &H1     'Set address and group address (2 bytes)
    'Public Const DEF_STAT as byte = &H2     'Define status items to return (1 byte)
    'Public Const READ_STAT as byte = &H3     'Read value of current status items
    'Public Const LOAD_TRAJ as byte = &H4       'Load trajectory data
    'Public Const START_MOVE as byte = &H5     'Start pre-loaded trajectory (0 bytes)
    Public Const SET_PARAM As Byte = &H6      'Set operating parameters (6 bytes)
    'Public Const STOP_MOTOR as byte = &H7      'Stop motor (1 byte)
    Public Const SET_OUTPUTS As Byte = &H8     'Set output bits (1 byte)
    'Public Const SET_HOMING as byte = &H9      'Define homing mode (1 byte)
    'Public Const SET_BAUD as byte = &HA      'Set the baud rate (1 byte)
    Public Const RESERVED As Byte = &HB      '
    'Public Const SAVE_AS_HOME as byte = &HC    'Store the input bytes and timer val (0 bytes)
    Public Const NOT_USED As Byte = &HD
    'Public Const NOP as byte = &HE       'No operation - returns prev. defined status (0 bytes)
    'Public Const HARD_RESET as byte = &HF     'RESET - no status is returned

    '
    'Step Module STATUSITEMS bit definitions:
    '
    'Public Const SEND_POS as byte = &H1     '4 bytes data
    'Public Const SEND_AD as byte = &H2      '1 byte
    Public Const SEND_ST As Byte = &H4      '2 bytes
    Public Const SEND_INBYTE As Byte = &H8     '1 byte
    'Public Const SEND_HOME as byte = &H10    '4 bytes
    'Public Const SEND_ID as byte = &H20     '2 bytes

    '
    'Step Module LOAD_TRAJ control byte bit definitions:
    '
    'Public Const LOAD_POS as byte = &H1     '+4 bytes
    Public Const LOAD_SPEED As Byte = &H2     '+1 bytes
    'Public Const LOAD_ACC as byte = &H4     '+1 bytes
    Public Const LOAD_ST As Byte = &H8      '+3 bytes
    Public Const STEP_REV As Byte = &H10        'reverse dir
    'Public Const START_NOW as byte = &H80     '1 = start now, 0 = wait for START_MOVE command

    '
    'Step Module SET_PARAM operating mode byte bit definitions:
    '
    Public Const SPEED_8X As Byte = &H0     'use 8x timing
    Public Const SPEED_4X As Byte = &H1     'use 4x timing
    Public Const SPEED_2X As Byte = &H2     'use 2x timing
    Public Const SPEED_1X As Byte = &H3     'use 1x timing
    Public Const IGNORE_LIMITS As Byte = &H4   'Do not stop automatically on limit switches
    Public Const IGNORE_ESTOP As Byte = &H8     'Do not stop automatically on e-stop
    Public Const ESTOP_OFF As Byte = &H10    'Stop abrupt on estop or limit switch

    '
    'Step Module STOP_MOTOR control byte bit definitions:
    '
    'Public Const AMP_ENABLE as byte = &H1     '1 = raise amp enable output, 0 = lower amp enable
    'Public Const STOP_ABRUPT as byte = &H4     'set to stop motor immediately
    'Public Const STOP_SMOOTH as byte = &H8      'set to decellerate motor smoothly

    '
    'Step Module SET_HOMING control byte bit definitions:
    '
    'Public Const ON_LIMIT1 as byte = &H1     'home on change in limit 1
    'Public Const ON_LIMIT2 as byte = &H2     'home on change in limit 2
    'Public Const HOME_MOTOR_OFF as byte = &H4    'turn motor off when homed
    Public Const ON_HOMESW As Byte = &H8     'home on change in index
    'Public Const HOME_STOP_ABRUPT as byte = &H10  'stop abruptly when homed
    'Public Const HOME_STOP_SMOOTH as byte = &H20  'stop smoothly when homed

    '
    'Step Module Status byte bit definitions:
    '
    Public Const MOTOR_MOVING As Byte = &H1    'Set when motor is moving
    'Public Const CKSUM_ERROR as byte = &H2     'checksum error in received command
    Public Const AMP_ENABLED As Byte = &H4     'set amplifier is enabled
    'Public Const POWER_ON as byte = &H8     'set when motor power is on
    Public Const AT_SPEED As Byte = &H10    'set on excess pos. error (sticky bit)
    Public Const VEL_MODE_ON As Byte = &H20     'set when in velocity profile mode
    Public Const TRAP_MODE As Byte = &H40    'set when in trap. profile mode
    'Public Const HOME_IN_PROG as byte = &H80    'set while searching for home, cleared when home found

    '
    'Step Module Input byte bit definitions:
    '
    Public Const ESTOP As Byte = &H1    'emergency stop input
    Public Const AUX_IN1 As Byte = &H2    'auxilliary input #1
    Public Const AUX_IN2 As Byte = &H2    'auxilliary input #2
    Public Const FWD_LIMIT As Byte = &H4   'forward limit switch
    Public Const REV_LIMIT As Byte = &H8     'reverse limit switch
    Public Const HOME_SWITCH As Byte = &H10  'homing limit switch

    '
    'Step module function prototypes:
    '
    Declare Sub StepNewMod Lib "nmclib04" ()
    Declare Function StepGetStat Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function StepGetPos Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function StepGetAD Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetStepTime Lib "nmclib04" (ByVal addr As Byte) As Short
    Declare Function StepGetInbyte Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetHome Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function StepGetCmdPos Lib "nmclib04" (ByVal addr As Byte) As Integer
    Declare Function StepGetCmdSpeed Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetCmdAcc Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetCmdST Lib "nmclib04" (ByVal addr As Byte) As Short
    Declare Function StepGetMinSpeed Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetOutputs Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetCtrlMode Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetRunCurrent Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetHoldCurrent Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetThermLimit Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetHomeCtrl Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepGetStopCtrl Lib "nmclib04" (ByVal addr As Byte) As Byte
    Declare Function StepSetParam Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte, ByVal minspeed As Byte, ByVal runcur As Byte, ByVal holdcur As Byte, ByVal thermlim As Byte) As Boolean
    Declare Function StepLoadTraj Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte, ByVal pos As Integer, ByVal speed As Byte, ByVal acc As Byte, ByVal raw_speed As Single) As Boolean
    Declare Function StepResetPos Lib "nmclib04" (ByVal addr As Byte) As Boolean
    Declare Function StepStopMotor Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte) As Boolean
    Declare Function StepSetOutputs Lib "nmclib04" (ByVal addr As Byte, ByVal outbyte As Byte) As Boolean
    Declare Function StepSetHoming Lib "nmclib04" (ByVal addr As Byte, ByVal mode As Byte) As Boolean


End Module
