using System;

namespace CRM.Core.Utils {
    public class NumGenerator {

        public static string UniqueCode(int length) {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "1234567890";

            string characters = alphabets + numbers;

            string random = string.Empty;
            for (int i = 0; i < length; i++) {
                string character;
                do {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (random.IndexOf(character) != -1);
                random += character;
            }

            string code = random;
            return code;
        }

        public static string UniqueOTP(int length) {
            string numbers = "1234567890";

            string characters = numbers;

            string random = string.Empty;
            for (int i = 0; i < length; i++) {
                string character;
                do {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (random.IndexOf(character) != -1);
                random += character;
            }

            string otp = random;
            return otp;
        }

        public static string UniqueNumber(int length) {
            string numbers = "1234567890";

            string characters = numbers;

            string random = string.Empty;
            for (int i = 0; i < length; i++) {
                string character;
                do {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (random.IndexOf(character) != -1);
                random += character;
            }

            string code = "GH" + random;
            return code;
        }

    }
}
