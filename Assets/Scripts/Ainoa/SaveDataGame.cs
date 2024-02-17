namespace Ainoa.Data
{
    [System.Serializable]
    public class SaveDataGame
    {
        internal int GetBestTimePlayed()
        {
            return _bestTimePlayed;
        }

        internal void SetBestTimePlayed(int value)
        {
            _bestTimePlayed = value;
        }

        private int _bestTimePlayed;   
    }
}