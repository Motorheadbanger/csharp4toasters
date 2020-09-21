namespace addressbook_web_tests
{
    class Square : Figure
    {
        public Square(int size)
        {
            Size = size;
        }

        public int Size { get; set; }
    }
}
