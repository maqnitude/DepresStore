namespace DepresStore.Storefront.ViewModels
{
    public class CategoryViewModel
    {
        public required string Name { get; set; }

        public List<CategoryViewModel> Subcategories { get; set; } = [];

        public static List<CategoryViewModel> GetTestCategories()
        {
            var categories = new List<CategoryViewModel>
            {
                new()
                {
                    Name = "Laptops",
                    Subcategories =
                    [
                        new() { Name = "Office Laptops" },
                        new() { Name = "Gaming Laptops" }
                    ]
                },
                new()
                {
                    Name = "Consoles",
                    Subcategories =
                    [
                        new() { Name = "Home Consoles" },
                        new() { Name = "Handheld Consoles" }
                    ]
                },
                new()
                {
                    Name = "Category",
                    Subcategories =
                    [
                        new()
                        {
                            Name = "Subcategory 1",
                            Subcategories =
                            [
                                new() { Name = "Subcategory 1.1" },
                                new() { Name = "Subcategory 1.2" }
                            ]
                        },
                        new()
                        {
                            Name = "Subcategory 2",
                            Subcategories =
                            [
                                new() { Name = "Subcategory 2.1" },
                                new() { Name = "Subcategory 2.2" },
                                new() { Name = "Subcategory 2.3" },
                            ]
                        }
                    ]
                }
            };

            return categories;
        }
    }
}