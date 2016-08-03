using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.Models
{
    public class MonoalphabeticCipherBusinessLayer
    {
        char[] alphabet;    //characters that will be encrypted

        //Constructor
        public MonoalphabeticCipherBusinessLayer()
        {
            alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
                'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        }


        /// <summary>
        /// Encrypts text using pattern text.
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="pattern">Pattern text</param>
        /// <returns>Encrypted text</returns>
        public CipherAndKey EncryptText(string plainText, string pattern)
        {
            string cipher = string.Empty;
            Dictionary<char, char> key = new Dictionary<char, char>();
            CipherAndKey result = new CipherAndKey();
            Dictionary<char, int> frequencyOfLetter = FrequencyOfLetters(pattern);

            key = CreateKey(frequencyOfLetter);

            for (int i = 0; i < plainText.Length; i++)
            {
                if (alphabet.Contains(plainText[i]))
                {
                    cipher += key[plainText[i]];
                }
                else if (alphabet.Contains(char.ToLowerInvariant(plainText[i])))
                {
                    cipher += char.ToUpperInvariant(key[char.ToLowerInvariant(plainText[i])]);
                }
                else
                {
                    cipher += plainText[i];
                }
            }

            result.Cipher = cipher;
            result.Key = key;

            return result;
        }


        /// <summary>
        /// Creates key based frequency of letters.
        /// </summary>
        /// <param name="dictFrequencyOfLetters"></param>
        /// <returns>Dictionary, where key is plain letter and value is encrypted letter</returns>
        public Dictionary<char, char> CreateKey(Dictionary<char, int> dictFrequencyOfLetters)
        {
            List<char> key = new List<char>();
            Dictionary<char, char> dictKey = new Dictionary<char, char>();

            key = dictFrequencyOfLetters.Keys.ToList();

            for (int i = 0; i < alphabet.Length; i++)
            {
                dictKey.Add(alphabet[i], key[i]);
            }

            return dictKey;
        }


        /// <summary>
        /// Counts how many times letter occurs in text, without case insensitive
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Sorted descending dictionary according the number of occurrences of letters in the text,
        /// where key is letter and value is number of occurrence this letter</returns>
        public Dictionary<char, int> FrequencyOfLetters(string text)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                frequency.Add(alphabet[i], 0);
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (alphabet.Contains(char.ToUpperInvariant(text[i])) || alphabet.Contains(char.ToLowerInvariant(text[i])))
                {
                    frequency[char.ToLowerInvariant(text[i])] += 1;
                }
            }

            frequency = frequency.OrderByDescending(v => v.Value).ToDictionary(v => v.Key, v => v.Value);

            return frequency;
        }
    }
}