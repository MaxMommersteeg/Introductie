namespace SudokuBasis {
    public class Position
    {
        public Position() {
            X = -1;
            Y = -1;
            Value = -1;
        }

        public short X { get; set; }

        public short Y { get; set; }

        public short Value { get; set; }

        public bool IsEditable { get; set; }
    }
}
