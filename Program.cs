using Org.BouncyCastle.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            string[] moves = args;
            int movesCount = moves.Length;
            int compChoiceIdx = rnd.Next(0, movesCount);    
            if(movesCount % 2 == 0 || movesCount < 3)
            {
                Console.WriteLine("Not valid data");
                return;
            }

            byte[] HMACkeyBytes = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(HMACkeyBytes);
            string HMACKey = String.Join("", BitConverter.ToString(HMACkeyBytes).Split('-'));

            var hash = new HMACSHA256(HMACkeyBytes);
            byte[] compChoiceBytes = Encoding.ASCII.GetBytes(moves[compChoiceIdx]);
            byte[] HMACValue = hash.ComputeHash(compChoiceBytes);
            Console.WriteLine("HMAC:");
            Console.WriteLine(String.Join("", BitConverter.ToString(HMACValue).Split('-')));


            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {moves[i]}");
            }
            Console.WriteLine($"0. exit");
            Console.WriteLine("Enter your move:");
            int userChoiceIdx;
            while (true)
            {
                int userChoiceIdxTemp = Convert.ToInt32(Console.ReadLine());
                if (userChoiceIdxTemp == 0)
                {
                    return;
                }
                if (userChoiceIdxTemp > 0 && userChoiceIdxTemp <= movesCount)
                {
                    userChoiceIdx = userChoiceIdxTemp - 1;
                    break;
                }
            }
            Console.WriteLine($"Your move: {moves[userChoiceIdx]}");
            Console.WriteLine($"Computer move: {moves[compChoiceIdx]}");

            userChoiceIdx++;
            compChoiceIdx++;

            if (userChoiceIdx < (movesCount / 2) + 1)
            {
                if (compChoiceIdx - userChoiceIdx < movesCount / 2)
                {
                    Console.WriteLine("Comp Win");
                }else{
                    Console.WriteLine("You Win");
                }
            } else if(userChoiceIdx > (movesCount / 2) + 1) {
                if (userChoiceIdx - compChoiceIdx < movesCount / 2)
                {
                    Console.WriteLine("You Win");
                }
                else
                {
                    Console.WriteLine("Comp Win");
                }
            } else
            {
                if (compChoiceIdx < userChoiceIdx)
                {
                    Console.WriteLine("You Win");
                }
                else if (compChoiceIdx > userChoiceIdx)
                {
                    Console.WriteLine("Comp Win");
                } else
                {
                    Console.WriteLine("Draw");
                }
            }

            Console.WriteLine($"HMAC key: {HMACKey}");
        }
    }
}
