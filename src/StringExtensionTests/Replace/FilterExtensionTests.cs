using NUnit.Framework;
using String_Extensions;

namespace StringClusterScanTests.Replace;

[TestFixture]
public class FilterExtensionTests
{
    [Test]
    [TestCase(
        "This is not a short story about a person named Not, who was not short but tall.",
        new[]{"not", "short", "but"},
        "This is  a  story about a person named Not, who was    tall.")]
    public void removing_instances_of_substrings_case_sensitive (string src, string[] unwanted, string expected)
    {
        Assert.That(src.Remove(unwanted), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(
        "This is not a short story about a person named Not, who was not short but tall.",
        new[]{"not", "short", "but"},
        "This is  a  story about a person named , who was    tall.")]
    public void removing_instances_of_substrings_case_insensitive (string src, string[] unwanted, string expected)
    {
        Assert.That(src.RemoveCaseInvariant(unwanted), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(
        "In 2024 at 12.15pm, we found 7 stone lions in a cave 250 miles from 9th Avenue",
        "In  at .pm, we found  stone lions in a cave  miles from th Avenue")]
    public void removing_numbers_from_strings (string src, string expected)
    {
        Assert.That(src.RemoveNumbers(), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(
        "In 2024, we found 7 stone lions in a cave 250 miles from 9th Avenue.",
        "In2024wefound7stonelionsinacave250milesfrom9thAvenue")]
    public void removing_non_alpha_numeric (string src, string expected)
    {
        Assert.That(src.RemoveNonAlphaNumeric(), Is.EqualTo(expected));
    }

    [Test]
    public void filtering_by_character ()
    {
        var src = "Hello, World! How are you?";
        var expected = "H, W! H  ?";
        Assert.That(src.FilterCharacters(char.IsLower), Is.EqualTo(expected));
    }

    [Test]
    public void reduce_all_whitespace_to_single_space ()
    {
        var src = "This\tis\u00a0a  story about \r\n a \rperson\n\t\r\n named Not, who was    tall.";
        var expected = "This is a story about a person named Not, who was tall.";
        Assert.That(src.NormaliseWhitespace(), Is.EqualTo(expected));
    }
}