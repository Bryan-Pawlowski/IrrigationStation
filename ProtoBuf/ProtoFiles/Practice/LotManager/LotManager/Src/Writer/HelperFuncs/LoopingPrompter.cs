using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Collections;

namespace LotManager
{
        /// <summary>
        /// Static class for holding a bunch of different console prompting patterns we might want to use.
        /// I just thought of this like an hour ago, so I'm very, very sorry if it seems half-baked. If you do feel that
        /// way, though, that's probably because... Well... it _is_ half-baked.
        /// </summary>
        public static class LoopingPrompter
        {
                /// <summary>
                /// Genericized method for handling a prompt for a selection of things from a list.
                /// The nature of this method is to repeat the prompt until a valid selection from the set is made.
                /// </summary>
                /// <param name="collection">
                /// Generic 'collection' from which we want the user to make a selection.
                /// </param>
                /// <param name="PromptMessage">
                /// Programmer-defined message to prompt the user to make a selection. This param has a default value,
                /// however, this default message is not very descriptive.
                /// </param>
                /// <typeparam name="T">
                /// Singular type which the collection contains.
                /// </typeparam>
                /// <returns>
                /// Selection index of type <b>int</b>
                /// </returns>
                public static int ChooseFromSet<T>(ICollection<T> collection, string PromptMessage = "Choose a thing from the list: ")
                {
                        bool ValidInput = false;

                        int selection = -1;
                        
                        StringBuilder myStringBuilder = new StringBuilder();
                        var members = collection.ToArray();
                        for (int i = 0; i < members.Length; i++)
                        {
                                myStringBuilder.Append($"[{i}] {members[i].ToString()}\n");
                        }

                        while (!ValidInput)
                        {
                                Console.Write(myStringBuilder.ToString());
                                Console.Write(PromptMessage);

                                var input = Console.ReadLine();
                                ValidInput = int.TryParse(input, out var num);

                                if (!ValidInput)
                                {
                                        continue;
                                }
                                else if (num < 0 || num >= members.Length)
                                {
                                        ValidInput = false;
                                }
                                selection = num;
                        }

                        Dictionary<int, string> dummyDict = new Dictionary<int, string>();
                        return selection;
                }

                /// <summary>
                /// Dictionary version of <see cref="ChooseFromSet{T}"/>. This function behaves exactly the same way
                /// as the generic list version of this function, we just need to implement a way to handle a dictionary
                /// instead of a list.
                /// </summary>
                /// <param name="collection"></param>
                /// <param name="PromptMessage"></param>
                /// <typeparam name="TKey"></typeparam>
                /// <typeparam name="TValue"></typeparam>
                /// <returns></returns>
                public static int ChooseFromSet<TKey, TValue>(ICollection<KeyValuePair<TKey, TValue>> collection,
                        string PromptMessage = "Choose a thing from the list: ")
                {
                        return -1;
                }

                /// <summary>
                /// Asks the user for an integer number.
                /// </summary>
                /// <param name="PromptMessage">
                /// Programmer-defined message to prompt the user to make a selection. This param has a default value,
                /// however, this default message is not very descriptive.
                /// </param>
                /// <returns>
                /// </returns>
                public static int AskForInt(string PromptMessage = "")
                {
                        return int.MinValue;
                }

                /// <summary>
                /// Asks the user for a float number.
                /// If the user does not input a valid number, this method will repeat itself.
                /// </summary>
                /// <param name="PromptMessage">
                /// Programmer-defined message to prompt the user to make a selection. This param has a default value,
                /// however, this default message is not very descriptive.
                /// </param>
                /// <returns>
                /// </returns>
                public static float AskForFloat(string PromptMessage = "")
                {
                        return float.MinValue;
                }
                
                
                
                public static string AskForAlphaNumbericString(
                        string PromptMessage = "Please give me an alphanumeric string:")
                {
                        return "";
                }
        }
}