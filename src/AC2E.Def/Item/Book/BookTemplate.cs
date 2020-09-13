namespace AC2E.Def {

    public class BookTemplate : Book {

        public override PackageType packageType => PackageType.BookTemplate;

        public BookTemplate(AC2Reader data) : base(data) {

        }
    }
}
