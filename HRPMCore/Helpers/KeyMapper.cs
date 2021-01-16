using HRPMSharedLibrary.Enums;
using System.Windows.Forms;

namespace HRPMCore.Helpers
{
    public class KeyMapper
    {
        //public static int GetKeyIndex(Keys key)
        //{
        //    int index = 0;
        //    switch (key)
        //    {
        //        case Keys.D0:
        //            index = 0;
        //            break;
        //        case Keys.D1:
        //            index = 1;
        //            break;
        //        case Keys.D2:
        //            index = 2;
        //            break;
        //        case Keys.D3:
        //            index = 3;
        //            break;
        //        case Keys.D4:
        //            index = 4;
        //            break;
        //        case Keys.D5:
        //            index = 5;
        //            break;
        //        case Keys.D6:
        //            index = 6;
        //            break;
        //        case Keys.D7:
        //            index = 7;
        //            break;
        //        case Keys.D8:
        //            index = 8;
        //            break;
        //        case Keys.D9:
        //            index = 9;
        //            break;
        //        case Keys.A:
        //            index = 10;
        //            break;
        //        case Keys.B:
        //            index = 11;
        //            break;
        //        case Keys.C:
        //            index = 12;
        //            break;
        //        case Keys.OemPeriod: // Ç
        //            index = 13;
        //            break;
        //        case Keys.D:
        //            index = 14;
        //            break;
        //        case Keys.E:
        //            index = 15;
        //            break;
        //        case Keys.F:
        //            index = 16;
        //            break;
        //        case Keys.G:
        //            index = 17;
        //            break;
        //        case Keys.OemOpenBrackets: // Ğ
        //            index = 18;
        //            break;
        //        case Keys.H:
        //            index = 19;
        //            break;
        //        case Keys.I:
        //            index = 20;
        //            break;
        //        case Keys.Oem7: // İ
        //            index = 21;
        //            break;
        //        case Keys.J:
        //            index = 22;
        //            break;
        //        case Keys.K:
        //            index = 23;
        //            break;
        //        case Keys.L:
        //            index = 24;
        //            break;
        //        case Keys.M:
        //            index = 25;
        //            break;
        //        case Keys.N:
        //            index = 26;
        //            break;
        //        case Keys.O:
        //            index = 27;
        //            break;
        //        case Keys.Oemcomma: // Ö 
        //            index = 28;
        //            break;
        //        case Keys.P:
        //            index = 29;
        //            break;
        //        case Keys.Q:
        //            index = 30;
        //            break;
        //        case Keys.R:
        //            index = 31;
        //            break;
        //        case Keys.S:
        //            index = 32;
        //            break;
        //        case Keys.Oem1: // Ş
        //            index = 33;
        //            break;
        //        case Keys.T:
        //            index = 34;
        //            break;
        //        case Keys.U:
        //            index = 35;
        //            break;
        //        case Keys.Oem6: // Ü
        //            index = 36;
        //            break;
        //        case Keys.V:
        //            index = 37;
        //            break;
        //        case Keys.W:
        //            index = 38;
        //            break;
        //        case Keys.X:
        //            index = 39;
        //            break;
        //        case Keys.Y:
        //            index = 40;
        //            break;
        //        case Keys.Z:
        //            index = 41;
        //            break;
        //        case Keys.OemMinus: // *
        //            index = 42;
        //            break;
        //        case Keys.Oemplus: // -
        //            index = 43;
        //            break;
        //        case Keys.Oem5: // ,
        //            index = 44;
        //            break;  
        //        case Keys.Oem2: // .
        //            index = 45;
        //            break;
        //        case Keys.OemBackslash: // <
        //            index = 46;
        //            break;
        //        case Keys.Oemtilde: // "
        //            index = 47;
        //            break;
        //        case Keys.Back: // Backspace
        //            index = 48;
        //            break;
        //        case Keys.Tab:
        //            index = 49;
        //            break;
        //        case Keys.CapsLock:
        //            index = 50;
        //            break;
        //        case Keys.Enter:
        //            index = 51;
        //            break;
        //        case Keys.Escape:
        //            index = 52;
        //            break;
        //        case Keys.F1:
        //            index = 53;
        //            break;
        //        case Keys.F2:
        //            index = 54;
        //            break;
        //        case Keys.F3:
        //            index = 55;
        //            break;
        //        case Keys.F4:
        //            index = 56;
        //            break;
        //        case Keys.F5:
        //            index = 57;
        //            break;
        //        case Keys.F6:
        //            index = 58;
        //            break;
        //        case Keys.F7:
        //            index = 59;
        //            break;
        //        case Keys.F8:
        //            index = 60;
        //            break;
        //        case Keys.F9:
        //            index = 61;
        //            break;
        //        case Keys.F10:
        //            index = 62;
        //            break;
        //        case Keys.F11:
        //            index = 63;
        //            break;
        //        case Keys.F12:
        //            index = 64;
        //            break;
        //        case Keys.Insert:
        //            index = 65;
        //            break;
        //        case Keys.Delete:
        //            index = 66;
        //            break;
        //        case Keys.Home:
        //            index = 67;
        //            break;
        //        case Keys.End:
        //            index = 68;
        //            break;
        //        case Keys.PageUp:
        //            index = 69;
        //            break;
        //        case Keys.PageDown:
        //            index = 70;
        //            break;
        //        case Keys.PrintScreen:
        //            index = 71;
        //            break;
        //        case Keys.Scroll:
        //            index = 72;
        //            break;
        //        case Keys.Pause:
        //            index = 73;
        //            break;
        //        case Keys.NumLock:
        //            index = 74;
        //            break;
        //        case Keys.NumPad0:
        //            index = 75;
        //            break;
        //        case Keys.NumPad1:
        //            index = 76;
        //            break;
        //        case Keys.NumPad2:
        //            index = 77;
        //            break;
        //        case Keys.NumPad3:
        //            index = 78;
        //            break;
        //        case Keys.NumPad4:
        //            index = 79;
        //            break;
        //        case Keys.NumPad5:
        //            index = 80;
        //            break;
        //        case Keys.NumPad6:
        //            index = 81;
        //            break;
        //        case Keys.NumPad7:
        //            index = 82;
        //            break;
        //        case Keys.NumPad8:
        //            index = 83;
        //            break;
        //        case Keys.NumPad9:
        //            index = 84;
        //            break;
        //        case Keys.Multiply:
        //            index = 85;
        //            break;
        //        case Keys.Add:
        //            index = 86;
        //            break;
        //        case Keys.Subtract:
        //            index = 87;
        //            break;
        //        case Keys.Divide:
        //            index = 88;
        //            break;
        //        case Keys.Decimal:
        //            index = 89;
        //            break;
        //        case Keys.Up:
        //            index = 90;
        //            break;
        //        case Keys.Down:
        //            index = 91;
        //            break;
        //        case Keys.Left:
        //            index = 92;
        //            break;
        //        case Keys.Right:
        //            index = 93;
        //            break;
        //        case Keys.LShiftKey:
        //            index = 94;
        //            break;
        //        case Keys.RShiftKey:
        //            index = 95;
        //            break;
        //        case Keys.LControlKey:
        //            index = 96;
        //            break;
        //        case Keys.RControlKey:
        //            index = 97;
        //            break;
        //        case Keys.LMenu:
        //            index = 98;
        //            break;
        //        case Keys.RMenu:
        //            index = 99;
        //            break;
        //        case Keys.LWin:
        //            index = 100;
        //            break;
        //        case Keys.RWin:
        //            index = 101;
        //            break;
        //        case Keys.Space:
        //            index = 102;
        //            break;
        //        default:
        //            index = -1;
        //            break;
        //    }
        //    return index;
        //}

        public static KeysList GetKeyEnum(Keys k)
        {
            KeysList key;
            switch (k)
            {
                case Keys.D0:
                    key = KeysList.D0;
                    break;
                case Keys.D1:
                    key = KeysList.D1;
                    break;
                case Keys.D2:
                    key = KeysList.D2;
                    break;
                case Keys.D3:
                    key = KeysList.D3;
                    break;
                case Keys.D4:
                    key = KeysList.D4;
                    break;
                case Keys.D5:
                    key = KeysList.D5;
                    break;
                case Keys.D6:
                    key = KeysList.D6;
                    break;
                case Keys.D7:
                    key = KeysList.D7;
                    break;
                case Keys.D8:
                    key = KeysList.D8;
                    break;
                case Keys.D9:
                    key = KeysList.D9;
                    break;
                case Keys.A:
                    key = KeysList.A;
                    break;
                case Keys.B:
                    key = KeysList.B;
                    break;
                case Keys.C:
                    key = KeysList.C;
                    break;
                case Keys.OemPeriod: // Ç
                    key = KeysList.OemPeriod;
                    break;
                case Keys.D:
                    key = KeysList.D;
                    break;
                case Keys.E:
                    key = KeysList.E;
                    break;
                case Keys.F:
                    key = KeysList.F;
                    break;
                case Keys.G:
                    key = KeysList.G;
                    break;
                case Keys.OemOpenBrackets: // Ğ
                    key = KeysList.OemOpenBrackets;
                    break;
                case Keys.H:
                    key = KeysList.H;
                    break;
                case Keys.I:
                    key = KeysList.I;
                    break;
                case Keys.Oem7: // İ
                    key = KeysList.Oem7;
                    break;
                case Keys.J:
                    key = KeysList.J;
                    break;
                case Keys.K:
                    key = KeysList.K;
                    break;
                case Keys.L:
                    key = KeysList.L;
                    break;
                case Keys.M:
                    key = KeysList.M;
                    break;
                case Keys.N:
                    key = KeysList.N;
                    break;
                case Keys.O:
                    key = KeysList.O;
                    break;
                case Keys.Oemcomma: // Ö 
                    key = KeysList.Oemcomma;
                    break;
                case Keys.P:
                    key = KeysList.P;
                    break;
                case Keys.Q:
                    key = KeysList.Q;
                    break;
                case Keys.R:
                    key = KeysList.R;
                    break;
                case Keys.S:
                    key = KeysList.S;
                    break;
                case Keys.Oem1: // Ş
                    key = KeysList.Oem1;
                    break;
                case Keys.T:
                    key = KeysList.T;
                    break;
                case Keys.U:
                    key = KeysList.U;
                    break;
                case Keys.Oem6: // Ü
                    key = KeysList.Oem6;
                    break;
                case Keys.V:
                    key = KeysList.V;
                    break;
                case Keys.W:
                    key = KeysList.W;
                    break;
                case Keys.X:
                    key = KeysList.X;
                    break;
                case Keys.Y:
                    key = KeysList.Y;
                    break;
                case Keys.Z:
                    key = KeysList.Z;
                    break;
                case Keys.OemMinus: // *
                    key = KeysList.OemMinus;
                    break;
                case Keys.Oemplus: // -
                    key = KeysList.Oemplus;
                    break;
                case Keys.Oem5: // ,
                    key = KeysList.Oem5;
                    break;
                case Keys.Oem2: // .
                    key = KeysList.Oem2;
                    break;
                case Keys.OemBackslash: // <
                    key = KeysList.OemBackslash;
                    break;
                case Keys.Oemtilde: // "
                    key = KeysList.Oemtilde;
                    break;
                case Keys.Back: // Backspace
                    key = KeysList.Back;
                    break;
                case Keys.Tab:
                    key = KeysList.Tab;
                    break;
                case Keys.CapsLock:
                    key = KeysList.CapsLock;
                    break;
                case Keys.Enter:
                    key = KeysList.Enter;
                    break;
                case Keys.Escape:
                    key = KeysList.Escape;
                    break;
                case Keys.F1:
                    key = KeysList.F1;
                    break;
                case Keys.F2:
                    key = KeysList.F2;
                    break;
                case Keys.F3:
                    key = KeysList.F3;
                    break;
                case Keys.F4:
                    key = KeysList.F4;
                    break;
                case Keys.F5:
                    key = KeysList.F5;
                    break;
                case Keys.F6:
                    key = KeysList.F6;
                    break;
                case Keys.F7:
                    key = KeysList.F7;
                    break;
                case Keys.F8:
                    key = KeysList.F8;
                    break;
                case Keys.F9:
                    key = KeysList.F9;
                    break;
                case Keys.F10:
                    key = KeysList.F10;
                    break;
                case Keys.F11:
                    key = KeysList.F11;
                    break;
                case Keys.F12:
                    key = KeysList.F12;
                    break;
                case Keys.Insert:
                    key = KeysList.Insert;
                    break;
                case Keys.Delete:
                    key = KeysList.Delete;
                    break;
                case Keys.Home:
                    key = KeysList.Home;
                    break;
                case Keys.End:
                    key = KeysList.End;
                    break;
                case Keys.PageUp:
                    key = KeysList.PageUp;
                    break;
                case Keys.PageDown:
                    key = KeysList.PageDown;
                    break;
                case Keys.PrintScreen:
                    key = KeysList.PrintScreen;
                    break;
                case Keys.Scroll:
                    key = KeysList.Scroll;
                    break;
                case Keys.Pause:
                    key = KeysList.Pause;
                    break;
                case Keys.NumLock:
                    key = KeysList.NumLock;
                    break;
                case Keys.NumPad0:
                    key = KeysList.NumPad0;
                    break;
                case Keys.NumPad1:
                    key = KeysList.NumPad1;
                    break;
                case Keys.NumPad2:
                    key = KeysList.NumPad2;
                    break;
                case Keys.NumPad3:
                    key = KeysList.NumPad3;
                    break;
                case Keys.NumPad4:
                    key = KeysList.NumPad4;
                    break;
                case Keys.NumPad5:
                    key = KeysList.NumPad5;
                    break;
                case Keys.NumPad6:
                    key = KeysList.NumPad6;
                    break;
                case Keys.NumPad7:
                    key = KeysList.NumPad7;
                    break;
                case Keys.NumPad8:
                    key = KeysList.NumPad8;
                    break;
                case Keys.NumPad9:
                    key = KeysList.NumPad9;
                    break;
                case Keys.Multiply:
                    key = KeysList.Multiply;
                    break;
                case Keys.Add:
                    key = KeysList.Add;
                    break;
                case Keys.Subtract:
                    key = KeysList.Subtract;
                    break;
                case Keys.Divide:
                    key = KeysList.Divide;
                    break;
                case Keys.Decimal:
                    key = KeysList.Decimal;
                    break;
                case Keys.Up:
                    key = KeysList.Up;
                    break;
                case Keys.Down:
                    key = KeysList.Down;
                    break;
                case Keys.Left:
                    key = KeysList.Left;
                    break;
                case Keys.Right:
                    key = KeysList.Right;
                    break;
                case Keys.LShiftKey:
                    key = KeysList.LShiftKey;
                    break;
                case Keys.RShiftKey:
                    key = KeysList.RShiftKey;
                    break;
                case Keys.LControlKey:
                    key = KeysList.LControlKey;
                    break;
                case Keys.RControlKey:
                    key = KeysList.RControlKey;
                    break;
                case Keys.LMenu:
                    key = KeysList.LMenu;
                    break;
                case Keys.RMenu:
                    key = KeysList.RMenu;
                    break;
                case Keys.LWin:
                    key = KeysList.LWin;
                    break;
                case Keys.RWin:
                    key = KeysList.RWin;
                    break;
                case Keys.Space:
                    key = KeysList.Space;
                    break;
                case Keys.Oem8:
                    key = KeysList.Oem8;
                    break;
                default:
                    key = KeysList.NoKey;
                    break;
            }
            return key;
        }
    }
}
