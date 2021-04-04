namespace Urho3DNet.InputEvents
{
    public enum UniKey
    {
        KeyUnknown = 0,
        KeyBackspace = 8,
        KeyTab = 9,
        KeyReturn = 13, // 0x0000000D
        KeyEscape = 27, // 0x0000001B
        KeySpace = 32, // 0x00000020
        KeyExclaim = 33, // 0x00000021
        KeyQuotedbl = 34, // 0x00000022
        KeyHash = 35, // 0x00000023
        KeyDollar = 36, // 0x00000024
        KeyPercent = 37, // 0x00000025
        KeyAmpersand = 38, // 0x00000026
        KeyQuote = 39, // 0x00000027
        KeyLeftparen = 40, // 0x00000028
        KeyRightparen = 41, // 0x00000029
        KeyAsterisk = 42, // 0x0000002A
        KeyPlus = 43, // 0x0000002B
        KeyComma = 44, // 0x0000002C
        KeyMinus = 45, // 0x0000002D
        KeyPeriod = 46, // 0x0000002E
        KeySlash = 47, // 0x0000002F
        Key0 = 48, // 0x00000030
        Key1 = 49, // 0x00000031
        Key2 = 50, // 0x00000032
        Key3 = 51, // 0x00000033
        Key4 = 52, // 0x00000034
        Key5 = 53, // 0x00000035
        Key6 = 54, // 0x00000036
        Key7 = 55, // 0x00000037
        Key8 = 56, // 0x00000038
        Key9 = 57, // 0x00000039
        KeyColon = 58, // 0x0000003A
        KeySemicolon = 59, // 0x0000003B
        KeyLess = 60, // 0x0000003C
        KeyEquals = 61, // 0x0000003D
        KeyGreater = 62, // 0x0000003E
        KeyQuestion = 63, // 0x0000003F
        KeyAt = 64, // 0x00000040
        KeyLeftbracket = 91, // 0x0000005B
        KeyBackslash = 92, // 0x0000005C
        KeyRightbracket = 93, // 0x0000005D
        KeyCaret = 94, // 0x0000005E
        KeyUnderscore = 95, // 0x0000005F
        KeyBackquote = 96, // 0x00000060
        KeyA = 97, // 0x00000061
        KeyB = 98, // 0x00000062
        KeyC = 99, // 0x00000063
        KeyD = 100, // 0x00000064
        KeyE = 101, // 0x00000065
        KeyF = 102, // 0x00000066
        KeyG = 103, // 0x00000067
        KeyH = 104, // 0x00000068
        KeyI = 105, // 0x00000069
        KeyJ = 106, // 0x0000006A
        KeyK = 107, // 0x0000006B
        KeyL = 108, // 0x0000006C
        KeyM = 109, // 0x0000006D
        KeyN = 110, // 0x0000006E
        KeyO = 111, // 0x0000006F
        KeyP = 112, // 0x00000070
        KeyQ = 113, // 0x00000071
        KeyR = 114, // 0x00000072
        KeyS = 115, // 0x00000073
        KeyT = 116, // 0x00000074
        KeyU = 117, // 0x00000075
        KeyV = 118, // 0x00000076
        KeyW = 119, // 0x00000077
        KeyX = 120, // 0x00000078
        KeyY = 121, // 0x00000079
        KeyZ = 122, // 0x0000007A
        KeyDelete = 127, // 0x0000007F
        KeyCapslock = 1073741881, // 0x40000039
        KeyF1 = 1073741882, // 0x4000003A
        KeyF2 = 1073741883, // 0x4000003B
        KeyF3 = 1073741884, // 0x4000003C
        KeyF4 = 1073741885, // 0x4000003D
        KeyF5 = 1073741886, // 0x4000003E
        KeyF6 = 1073741887, // 0x4000003F
        KeyF7 = 1073741888, // 0x40000040
        KeyF8 = 1073741889, // 0x40000041
        KeyF9 = 1073741890, // 0x40000042
        KeyF10 = 1073741891, // 0x40000043
        KeyF11 = 1073741892, // 0x40000044
        KeyF12 = 1073741893, // 0x40000045
        KeyPrintscreen = 1073741894, // 0x40000046
        KeyScrolllock = 1073741895, // 0x40000047
        KeyPause = 1073741896, // 0x40000048
        KeyInsert = 1073741897, // 0x40000049
        KeyHome = 1073741898, // 0x4000004A
        KeyPageup = 1073741899, // 0x4000004B
        KeyEnd = 1073741901, // 0x4000004D
        KeyPagedown = 1073741902, // 0x4000004E
        KeyRight = 1073741903, // 0x4000004F
        KeyLeft = 1073741904, // 0x40000050
        KeyDown = 1073741905, // 0x40000051
        KeyUp = 1073741906, // 0x40000052
        KeyNumlockclear = 1073741907, // 0x40000053
        KeyKpDivide = 1073741908, // 0x40000054
        KeyKpMultiply = 1073741909, // 0x40000055
        KeyKpMinus = 1073741910, // 0x40000056
        KeyKpPlus = 1073741911, // 0x40000057
        KeyKpEnter = 1073741912, // 0x40000058
        KeyKp1 = 1073741913, // 0x40000059
        KeyKp2 = 1073741914, // 0x4000005A
        KeyKp3 = 1073741915, // 0x4000005B
        KeyKp4 = 1073741916, // 0x4000005C
        KeyKp5 = 1073741917, // 0x4000005D
        KeyKp6 = 1073741918, // 0x4000005E
        KeyKp7 = 1073741919, // 0x4000005F
        KeyKp8 = 1073741920, // 0x40000060
        KeyKp9 = 1073741921, // 0x40000061
        KeyKp0 = 1073741922, // 0x40000062
        KeyKpPeriod = 1073741923, // 0x40000063
        KeyApplication = 1073741925, // 0x40000065
        KeyPower = 1073741926, // 0x40000066
        KeyKpEquals = 1073741927, // 0x40000067
        KeyF13 = 1073741928, // 0x40000068
        KeyF14 = 1073741929, // 0x40000069
        KeyF15 = 1073741930, // 0x4000006A
        KeyF16 = 1073741931, // 0x4000006B
        KeyF17 = 1073741932, // 0x4000006C
        KeyF18 = 1073741933, // 0x4000006D
        KeyF19 = 1073741934, // 0x4000006E
        KeyF20 = 1073741935, // 0x4000006F
        KeyF21 = 1073741936, // 0x40000070
        KeyF22 = 1073741937, // 0x40000071
        KeyF23 = 1073741938, // 0x40000072
        KeyF24 = 1073741939, // 0x40000073
        KeyHelp = 1073741941, // 0x40000075
        KeyMenu = 1073741942, // 0x40000076
        KeySelect = 1073741943, // 0x40000077
        KeyStop = 1073741944, // 0x40000078
        KeyAgain = 1073741945, // 0x40000079
        KeyUndo = 1073741946, // 0x4000007A
        KeyCut = 1073741947, // 0x4000007B
        KeyCopy = 1073741948, // 0x4000007C
        KeyPaste = 1073741949, // 0x4000007D
        KeyFind = 1073741950, // 0x4000007E
        KeyMute = 1073741951, // 0x4000007F
        KeyVolumeup = 1073741952, // 0x40000080
        KeyVolumedown = 1073741953, // 0x40000081
        KeyKpComma = 1073741957, // 0x40000085
        KeyKpEqualsas400 = 1073741958, // 0x40000086
        KeyAlterase = 1073741977, // 0x40000099
        KeySysreq = 1073741978, // 0x4000009A
        KeyCancel = 1073741979, // 0x4000009B
        KeyClear = 1073741980, // 0x4000009C
        KeyPrior = 1073741981, // 0x4000009D
        KeyReturn2 = 1073741982, // 0x4000009E
        KeySeparator = 1073741983, // 0x4000009F
        KeyOut = 1073741984, // 0x400000A0
        KeyOper = 1073741985, // 0x400000A1
        KeyClearagain = 1073741986, // 0x400000A2
        KeyCrsel = 1073741987, // 0x400000A3
        KeyExsel = 1073741988, // 0x400000A4
        KeyKp00 = 1073742000, // 0x400000B0
        KeyKp000 = 1073742001, // 0x400000B1
        KeyThousandsseparator = 1073742002, // 0x400000B2
        KeyDecimalseparator = 1073742003, // 0x400000B3
        KeyCurrencyunit = 1073742004, // 0x400000B4
        KeyCurrencysubunit = 1073742005, // 0x400000B5
        KeyKpLeftparen = 1073742006, // 0x400000B6
        KeyKpRightparen = 1073742007, // 0x400000B7
        KeyKpLeftbrace = 1073742008, // 0x400000B8
        KeyKpRightbrace = 1073742009, // 0x400000B9
        KeyKpTab = 1073742010, // 0x400000BA
        KeyKpBackspace = 1073742011, // 0x400000BB
        KeyKpA = 1073742012, // 0x400000BC
        KeyKpB = 1073742013, // 0x400000BD
        KeyKpC = 1073742014, // 0x400000BE
        KeyKpD = 1073742015, // 0x400000BF
        KeyKpE = 1073742016, // 0x400000C0
        KeyKpF = 1073742017, // 0x400000C1
        KeyKpXor = 1073742018, // 0x400000C2
        KeyKpPower = 1073742019, // 0x400000C3
        KeyKpPercent = 1073742020, // 0x400000C4
        KeyKpLess = 1073742021, // 0x400000C5
        KeyKpGreater = 1073742022, // 0x400000C6
        KeyKpAmpersand = 1073742023, // 0x400000C7
        KeyKpDblampersand = 1073742024, // 0x400000C8
        KeyKpVerticalbar = 1073742025, // 0x400000C9
        KeyKpDblverticalbar = 1073742026, // 0x400000CA
        KeyKpColon = 1073742027, // 0x400000CB
        KeyKpHash = 1073742028, // 0x400000CC
        KeyKpSpace = 1073742029, // 0x400000CD
        KeyKpAt = 1073742030, // 0x400000CE
        KeyKpExclam = 1073742031, // 0x400000CF
        KeyKpMemstore = 1073742032, // 0x400000D0
        KeyKpMemrecall = 1073742033, // 0x400000D1
        KeyKpMemclear = 1073742034, // 0x400000D2
        KeyKpMemadd = 1073742035, // 0x400000D3
        KeyKpMemsubtract = 1073742036, // 0x400000D4
        KeyKpMemmultiply = 1073742037, // 0x400000D5
        KeyKpMemdivide = 1073742038, // 0x400000D6
        KeyKpPlusminus = 1073742039, // 0x400000D7
        KeyKpClear = 1073742040, // 0x400000D8
        KeyKpClearentry = 1073742041, // 0x400000D9
        KeyKpBinary = 1073742042, // 0x400000DA
        KeyKpOctal = 1073742043, // 0x400000DB
        KeyKpDecimal = 1073742044, // 0x400000DC
        KeyKpHexadecimal = 1073742045, // 0x400000DD
        KeyCtrl = 1073742048, // 0x400000E0
        KeyLctrl = 1073742048, // 0x400000E0
        KeyLshift = 1073742049, // 0x400000E1
        KeyShift = 1073742049, // 0x400000E1
        KeyAlt = 1073742050, // 0x400000E2
        KeyLalt = 1073742050, // 0x400000E2
        KeyGui = 1073742051, // 0x400000E3
        KeyLgui = 1073742051, // 0x400000E3
        KeyRctrl = 1073742052, // 0x400000E4
        KeyRshift = 1073742053, // 0x400000E5
        KeyRalt = 1073742054, // 0x400000E6
        KeyRgui = 1073742055, // 0x400000E7
        KeyMode = 1073742081, // 0x40000101
        KeyAudionext = 1073742082, // 0x40000102
        KeyAudioprev = 1073742083, // 0x40000103
        KeyAudiostop = 1073742084, // 0x40000104
        KeyAudioplay = 1073742085, // 0x40000105
        KeyAudiomute = 1073742086, // 0x40000106
        KeyMediaselect = 1073742087, // 0x40000107
        KeyWww = 1073742088, // 0x40000108
        KeyMail = 1073742089, // 0x40000109
        KeyCalculator = 1073742090, // 0x4000010A
        KeyComputer = 1073742091, // 0x4000010B
        KeyAcSearch = 1073742092, // 0x4000010C
        KeyAcHome = 1073742093, // 0x4000010D
        KeyAcBack = 1073742094, // 0x4000010E
        KeyAcForward = 1073742095, // 0x4000010F
        KeyAcStop = 1073742096, // 0x40000110
        KeyAcRefresh = 1073742097, // 0x40000111
        KeyAcBookmarks = 1073742098, // 0x40000112
        KeyBrightnessdown = 1073742099, // 0x40000113
        KeyBrightnessup = 1073742100, // 0x40000114
        KeyDisplayswitch = 1073742101, // 0x40000115
        KeyKbdillumtoggle = 1073742102, // 0x40000116
        KeyKbdillumdown = 1073742103, // 0x40000117
        KeyKbdillumup = 1073742104, // 0x40000118
        KeyEject = 1073742105, // 0x40000119
        KeySleep = 1073742106, // 0x4000011A

        // Mouse buttons
        MouseButtonLeft = 0x10000001,
        MouseButtonRight,
        MouseButtonMiddle,
        MouseButton1,
        MouseButton2,

        // Joystick buttons
        Button0 = 0x20000000,
        Button1,
        Button2,
        Button3,
        Button4,
        Button5,
        Button6,
        Button7,
        Button8,
        Button9,
        Button10,
        Button11,
        Button12,
        Button13,
        Button14,
        Button15,
        Button16,
        Button17,
        Button18,
        Button19,
        Button20,
        Button21,
        Button22,
        Button23,
        Button24,
        Button25,
        Button26,
        Button27,
        Button28,
        Button29,
        Button30,
        Button31,

        // Gamepad buttons
        ButtonA,
        ButtonB,
        ButtonX,
        ButtonY,
        ButtonBack,
        ButtonGuide,
        ButtonStart,
        ButtonLeftStick,
        ButtonRightStick,
        ButtonLeftShoulder,
        ButtonRightShoulder,
        ButtonDpadUp,
        ButtonDPadDown,
        ButtonDPadLeft,
        ButtonDpadRight
    }
}