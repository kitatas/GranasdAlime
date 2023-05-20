using System;
using Tsutaeru.InGame.Data.Entity;

namespace Tsutaeru.InGame.Data.DataStore
{
    [Serializable]
    public sealed class QuestionTable
    {
        public QuestionEntity[] data_list;
    }
}