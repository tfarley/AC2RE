namespace AC2E.Def {

    public class Book : CItem {

        public override PackageType packageType => PackageType.Book;

        public Book(AC2Reader data) : base(data) {

        }
    }
}
