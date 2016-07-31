using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmallPrograms.Areas.AdvancedMethodsOfInformationProtection.Models
{
    public class DESBusinessLayer
    {
        readonly byte[,] initialIPPermutation, initialEPerputation, initialPC1Permutation, initialPC2Permutation, 
            initialPPermutation, initialIPPermutationInverse;

        readonly List<byte[,]> sBoxList; 

        //constructor
        public DESBusinessLayer()
        {
            initialIPPermutation = new byte[8, 8]
            {
                {58,50,42,34,26,18,10,2 },
                {60,52,44,36,28,20,12,4 },
                {62,54,46,38,30,22,14,6 },
                {64,56,48,40,32,24,16,8 },
                {57,49,41,33,25,17,9,1 },
                {59,51,43,35,27,19,11,3 },
                {61,53,45,37,29,21,13,5 },
                {63,55,47,39,31,23,15,7 }
            };

            initialEPerputation = new byte[8, 6]
            {
                {32,1,2,3,4,5 },
                {4,5,6,7,8,9 },
                {8,9,10,11,12,13 },
                {12,13,14,15,16,17 },
                {16,17,18,19,20,21 },
                {20,21,22,23,24,25 },
                {24,25,26,27,28,29 },
                {28,29,30,31,32,1 }
            };

            initialPC1Permutation = new byte[8, 7]
            {
                {57,49,41,33,25,17,9 },
                {1,58,50,42,34,26,18 },
                {10,2,59,51,43,35,27 },
                {19,11,3,60,52,44,36 },
                {63,55,47,39,31,23,15 },
                {7,62,54,46,38,30,22 },
                {14,6,61,53,45,37,29 },
                {21,13,5,28,20,12,4 }
            };

            initialPC2Permutation = new byte[8, 6]
            {
                {14,17,11,24,1,5 },
                {3,28,15,6,21,10 },
                {23,19,12,4,26,8 },
                {16,7,27,20,13,2 },
                {41,52,31,37,47,55 },
                {30,40,51,45,33,48 },
                {44,49,39,56,34,53 },
                {46,42,50,36,29,32 }
            };

            #region sbox initial

            sBoxList = new List<byte[,]>();
            byte[,] sBox1 =
            {
                {14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7 },
                {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8 },
                {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0 },
                {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13 }
            };
            byte[,] sBox2 =
            {
                {15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10 },
                {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5 },
                {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15 },
                {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9 }
            };
            byte[,] sBox3 =
            {
                {10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8 },
                {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1 },
                {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7 },
                {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12 }
            };
            byte[,] sBox4 =
            {
                {7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15 },
                {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9 },
                {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4 },
                {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14 }
            };
            byte[,] sBox5 =
            {
                {2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9 },
                {14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6 },
                {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14 },
                {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3 }
            };
            byte[,] sBox6 =
            {
                {12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11 },
                {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8 },
                {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6 },
                {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13 }
            };
            byte[,] sBox7 =
            {
                {4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1 },
                {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6 },
                {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2 },
                {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12 }
            };
            byte[,] sBox8 =
            {
                {13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7 },
                {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2 },
                {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8 },
                {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11 }
            };
            sBoxList.Add(sBox1);
            sBoxList.Add(sBox2);
            sBoxList.Add(sBox3);
            sBoxList.Add(sBox4);
            sBoxList.Add(sBox5);
            sBoxList.Add(sBox6);
            sBoxList.Add(sBox7);
            sBoxList.Add(sBox8);

            #endregion

            initialPPermutation = new byte[8, 4]
            {
                {16,7,20,21 },
                {29,12,28,17 },
                {1,15,23,26 },
                {5,18,31,10 },
                {2,8,24,14 },
                {32,27,3,9 },
                {19,13,30,6 },
                {22,11,4,25 }
            };

            initialIPPermutationInverse = new byte[8, 8]
            {
                {40,8,48,16,56,24,64,32 },
                {39,7,47,15,55,23,63,31 },
                {38,6,46,14,54,22,62,30 },
                {37,5,45,13,53,21,61,29 },
                {36,4,44,12,52,20,60,28 },
                {35,3,43,11,51,19,59,27 },
                {34,2,42,10,50,18,58,26 },
                {33,1,41,9,49,17,57,25 }
            };
        }


        /// <summary>
        /// Encrypts or decrypts text using DES algorithm.
        /// </summary>
        /// <param name="key">64-bit key</param>
        /// <param name="text">Text to encrypt or decrypt in binary format with a lenghth of 64 bits (64-element array of bits)</param>
        /// <param name="encrypt">True - if you want to encrypt text, false - if you want to decrypt text.</param>
        /// <returns>Encrypted or decrypted text in string</returns>
        public byte[] DESAlgorithm(byte[] key, byte[] text, bool encrypt)
        {
            List<byte[]> subkeyList = new List<byte[]>();
            byte[] xor;
            byte[] finalBlock = new byte[64];   //block obtained after 16 iteratoions

            subkeyList = CreateSubkeys(key);

            byte[] outputIPPermutation = Permutation(text, initialIPPermutation);
            Block b = ArrayShare(outputIPPermutation);
            byte[] leftSide = b.LeftSide;
            byte[] rightSide = b.RightSide;

            if (encrypt == true)
            {
                for (int i = 0; i < 16; i++)
                {
                    byte[] fFunction = Ffunction(rightSide, subkeyList[i]);
                    xor = XOR(fFunction, leftSide);

                    leftSide = rightSide;
                    rightSide = xor;
                }
            }

            if (encrypt == false)
            {
                for (int i = 15; i > -1; i--)
                {
                    byte[] fFunction = Ffunction(rightSide, subkeyList[i]);
                    xor = XOR(fFunction, leftSide);

                    leftSide = rightSide;
                    rightSide = xor;
                }
            }

            finalBlock = ConcatenateCDPair(rightSide, leftSide);
            byte[] desResult = Permutation(finalBlock, initialIPPermutationInverse);

            return desResult;
        }


        /// <summary>
        /// Divides array on list of 64-element arrays.
        /// If array size is not multiple 64, missing elements are fill 0 on the left side.
        /// </summary>
        /// <param name="array">Array to divide</param>
        /// <returns>List of 64-element arrays</returns>
        public List<byte[]> ArrayShareOnListOf64ElementsArrays(byte[] array)
        {
            List<byte[]> arrayList = new List<byte[]>();
            int listSize = (int)Math.Ceiling((double)(array.Length / 64.0));

            for (int i = 0; i < listSize; i++)
            {
                byte[] array64elements = new byte[64];

                if (i != listSize - 1)
                {
                    for (int arrayIndex = i * 64, array64Index = 0; arrayIndex < i * 64 + 64; arrayIndex++, array64Index++)
                    {
                        array64elements[array64Index] = array[arrayIndex];
                    }

                    arrayList.Add(array64elements);
                }
                else
                {
                    for (int arrayIndex = array.Length - 1, array64Index = 63; arrayIndex >= i * 64; arrayIndex--, array64Index--)
                    {
                        array64elements[array64Index] = array[arrayIndex];
                    }

                    arrayList.Add(array64elements);
                }
            }

            return arrayList;
        }


        public string ConvertArrayToString(byte[] array)
        {
            string sArray = string.Empty;

            foreach (var item in array)
            {
                sArray += item.ToString();
            }

            return sArray;
        }


        public byte[] Ffunction(byte[] rightSide, byte[] key)
        {
            byte[] outputEPermutation = Permutation(rightSide, initialEPerputation);
            byte[] xor = XOR(outputEPermutation, key);
            byte[,] xorAs2DArray = ConvertArrayTo2DArray(xor, 8, 6);
            int[,] coordinates = SboxCoordinates(xorAs2DArray);
            byte[] sboxOutput = SFunction(coordinates);
            byte[] outputPPermutation = Permutation(sboxOutput, initialPPermutation);

            return outputPPermutation;
        }


        public int[,] SboxCoordinates(byte[,] block)
        {
            int[,] coordinates = new int[block.GetLength(0), 2];
            for (int i = 0; i < block.GetLength(0); i++)
            {
                coordinates[i, 0] = Convert.ToInt32(block[i, 0].ToString() + block[i, 5].ToString(), 2);
                coordinates[i, 1] = Convert.ToInt32(block[i, 1].ToString() + block[i, 2].ToString()
                    + block[i, 3].ToString() + block[i, 4].ToString(), 2);
            }

            return coordinates;
        }


        public byte[] SFunction(int[,] coordinates)
        {
            byte[] output = new byte[32];
            string sOutput = "";

            for (int i = 0; i < sBoxList.Count(); i++)
            {
                int value = sBoxList[i][coordinates[i, 0], coordinates[i, 1]];
                sOutput += Convert.ToString(value, 2).PadLeft(4, '0');
            }
            for (int i = 0; i < sOutput.Length; i++)
            {
                output[i] = (byte)Char.GetNumericValue(sOutput[i]);
            }

            return output;
        }


        /// <summary>
        /// Converts 1D array to 2D array of specified dimensions 
        /// </summary>
        /// <param name="array">Array to convert</param>
        /// <param name="dim0">Number of rows</param>
        /// <param name="dim1">Number of columns</param>
        /// <returns>2D array</returns>
        public byte[,] ConvertArrayTo2DArray(byte[] array, int dim0, int dim1)
        {
            int k = 0;
            byte[,] array2D = new byte[dim0, dim1];

            for (int i = 0; i < dim0; i++)
            {
                for (int j = 0; j < dim1; j++)
                {
                    array2D[i, j] = array[k];
                    k++;
                }
            }

            return array2D;
        }


        /// <param name="p">Array of btis</param>
        /// <param name="q">Array of bits</param>
        public byte[] XOR(byte[] p, byte[] q)
        {
            byte[] xor = new byte[p.Length];

            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] == q[i])
                {
                    xor[i] = 0;
                }
                else
                {
                    xor[i] = 1;
                }
            }

            return xor;
        }


        /// <summary>
        /// Creates subkeys based on key
        /// </summary>
        /// <param name="key">Key in the binary system</param>
        /// <returns>List of subkeys</returns>
        public List<byte[]> CreateSubkeys(byte[] key)
        {
            List<Block> cdPairList = new List<Block>(); //C corresponds left side, D corresponds right side
            List<byte[]> subkeyList = new List<byte[]>();
            byte[] shift = new byte[16] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };
            byte[] keyAfterPC1Pemutation = new byte[48];

            keyAfterPC1Pemutation = Permutation(key, initialPC1Permutation);
            cdPairList.Add(ArrayShare(keyAfterPC1Pemutation));

            for (int i = 0; i < 16; i++)
            {
                Block b = new Block();

                b.LeftSide = ShiftBitsInLeft(shift[i], cdPairList[i].LeftSide);
                b.RightSide = ShiftBitsInLeft(shift[i], cdPairList[i].RightSide);

                cdPairList.Add(b);
            }

            for (int i = 1; i < cdPairList.Count; i++)
            {
                subkeyList.Add(Permutation(ConcatenateCDPair(cdPairList[i].LeftSide, cdPairList[i].RightSide), initialPC2Permutation));
            }

            return subkeyList;
        }

        public byte[] Permutation(byte[] input, byte[,] initialPermutation)
        {
            byte[] output = new byte[initialPermutation.Length];
            int indexer = 0;

            foreach (var item in initialPermutation)
            {
                output[indexer] = input[item - 1];
                indexer++;
            }

            //for (int i = 0; i < initialPermutation.GetLength(0); i++)
            //{
            //    for (int j = 0; j < initialPermutation.GetLength(1); j++)
            //    {
            //        output[indexer] = input[initialPermutation[i, j] - 1];
            //        indexer++;
            //    }
            //}

            return output;
        }


        /// <summary>
        /// Divides array on 2 equal parts - left and right
        /// </summary>
        /// <param name="array"></param>
        /// <returns>Divided array as Block model</returns>
        public Block ArrayShare(byte[] array)
        {
            Block b = new Block();
            int i;

            int half = array.Length / 2;
            b.LeftSide = new byte[half];
            b.RightSide = new byte[half];

            for (i = 0; i < half; i++)
            {
                b.LeftSide[i] = array[i];
            }
            for (int j = 0; i < array.Length; i++, j++)
            {
                b.RightSide[j] = array[i];
            }

            return b;
        }


        public byte[] ShiftBitsInLeft(int aboutHowMany, byte[] bits)
        {
            byte[] shiftedBits = new byte[bits.Length];
            int j = 0;

            for (int i = aboutHowMany; i < bits.Length; i++, j++)
            {
                shiftedBits[j] = bits[i];
            }
            for (int i = 0; i < aboutHowMany; i++, j++)
            {
                shiftedBits[j] = bits[i];
            }

            return shiftedBits;
        }


        /// <summary>
        /// Concatenates two arrays in one
        /// </summary>
        /// <param name="c">First array</param>
        /// <param name="d">Second array</param>
        /// <returns>Concatenated array</returns>
        public byte[] ConcatenateCDPair(byte[] c, byte[] d)
        {
            int indexer = 0;
            int size = c.Length * 2;
            byte[] merged = new byte[size];

            foreach (var item in c)
            {
                merged[indexer] = item;
                indexer++;
            }
            foreach (var item in d)
            {
                merged[indexer] = item;
                indexer++;
            }

            return merged;
        }


        /// <summary>
        /// Check if parameter is hexadecimal number.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if value is hexadecimal number, otherwise false</returns>
        public bool IsValidHex(string value)
        {
            string hexCharacters = "0123456789ABCDEFabcdef";

            foreach (var item in value)
            {
                if (hexCharacters.Contains(item)==false)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Check if parameter is binary number.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>True if value is binary number, otherwise false</returns>
        public bool IsValidBinary(string value)
        {
            string binaryCharacters = "01";

            foreach (var item in value)
            {
                if (binaryCharacters.Contains(item) == false)
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Gets every character in string and saves it in array.
        /// </summary>
        public byte[] ConvertStringToArray(string s)
        {
            byte[] array = new byte[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                array[i] = (byte)Char.GetNumericValue(s[i]);
            }

            return array;
        }
    }
}