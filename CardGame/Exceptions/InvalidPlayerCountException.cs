// (C) SAP 2021

using System;

namespace SAP.ProgrammingChallenge.CardGame.Exceptions
{
    public class InvalidPlayerCountException : Exception 
    {
        public InvalidPlayerCountException() : base("Player count should be more than 1")
        {
        }
    }
}
