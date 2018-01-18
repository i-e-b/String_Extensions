using System.Collections.Generic;
using NUnit.Framework;
using String_Extensions;

namespace StringClusterScanTests.Aggregation
{
    [TestFixture]
    public class DictionaryJoinTests
    {
        [Test]
        [TestCase(":",",", "k1:v1,k2:v2")]
        [TestCase("=","&", "k1=v1&k2=v2")]
        [TestCase("",";", "k1v1;k2v2")]
        [TestCase("","", "k1v1k2v2")]
        [TestCase("\r","", "k1\rv1k2\rv2")]
        public void joining_dictionary_entries_with_single_characters (string keyValueCombiner, string entryCombiner, string expected)
        {
            var dict = new Dictionary<string, string>{
                { "k1", "v1" },
                { "k2", "v2" }
            };
            
            var actual = dict.Join(keyValueCombiner, entryCombiner);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(" -> ","\r\n", "k1 -> v1\r\nk2 -> v2")]
        [TestCase("','","'},{'", "k1','v1'},{'k2','v2")]
        public void joining_dictionary_entries_with_strings (string keyValueCombiner, string entryCombiner, string expected)
        {
            var dict = new Dictionary<string, string>{
                { "k1", "v1" },
                { "k2", "v2" }
            };
            
            var actual = dict.Join(keyValueCombiner, entryCombiner);

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [Test]
        [TestCase("","", "")]
        [TestCase("=","&", "")]
        public void joining_empty_dictionary_results_in_empty_string (string keyValueCombiner, string entryCombiner, string expected)
        {
            var dict = new Dictionary<string, string>();
            
            var actual = dict.Join(keyValueCombiner, entryCombiner);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("","", "k1v1")]
        [TestCase("=","&", "k1=v1")]
        public void joining_single_entry_dictionary_doesnt_use_entry_combiner (string keyValueCombiner, string entryCombiner, string expected)
        {
            var dict = new Dictionary<string, string>{
                { "k1", "v1" }
            };
            
            var actual = dict.Join(keyValueCombiner, entryCombiner);

            Assert.That(actual, Is.EqualTo(expected));
        }

        
        [Test]
        [TestCase("","", "1System.Byte[]2System.Byte[]")]
        [TestCase("=","&", "1=System.Byte[]&2=System.Byte[]")]
        public void joining_non_string_dictionary_uses_default_to_string (string keyValueCombiner, string entryCombiner, string expected)
        {
            var dict = new Dictionary<int, byte[]>{
                { 1, new byte[]{ 1,2 } },
                { 2, new byte[]{ 3,4 } }
            };
            
            var actual = dict.Join(keyValueCombiner, entryCombiner);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void null_combiners_are_treated_as_empty ()
        {
            var dict = new Dictionary<string, string>{
                { "k1", "v1" },
                { "k2", "v2" }
            };

            var left = dict.Join(null, ":");
            var right = dict.Join(":", null);
            var both = dict.Join(null, null);

            Assert.That(left, Is.EqualTo("k1v1:k2v2"));
            Assert.That(right, Is.EqualTo("k1:v1k2:v2"));
            Assert.That(both, Is.EqualTo("k1v1k2v2"));
        }

        [Test]
        public void null_dictionary_results_in_null ()
        {
            Dictionary<string,string> nullDict = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.That(nullDict.Join("=","&"), Is.Null);
        }
    }
}