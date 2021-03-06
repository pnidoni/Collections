#region --- License & Copyright Notice ---
/*
Custom collections and collection extensions for .NET
Copyright (c) 2018-2019 Jeevan James
All rights reserved.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System;
using System.Collections.Generic;
using Collection.Tests.DataAttributes;
using Shouldly;
using Xunit;

namespace Collection.Tests.ShortCollectionExtensions
{
    public sealed class IndexOfSequences_Tests
    {
        [Theory, ShortArray(CollectionType.Null)]
        public void Throws_if_shorts_are_null(IList<short> shorts)
        {
            Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequences(1));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_start_is_negative(IList<short> shorts)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequences(-1, 10, 1));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_count_is_negative(IList<short> shorts)
        {
            Should.Throw<ArgumentOutOfRangeException>(() => shorts.IndexOfSequences(0, -1, 1));
        }

        [Theory, ShortArray(CollectionType.NonEmpty)]
        public void Throws_if_sequence_is_null(IList<short> shorts)
        {
            Should.Throw<ArgumentNullException>(() => shorts.IndexOfSequences(0, 100, null));
        }

        [Theory]
        [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 2, 3 }, new[] { 1, 4, 6 })]
        [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 2, 3, 1 }, new[] { 1 })]
        [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 1 }, new[] { 0, 3 })]
        public void Returns_indices_of_existing_sequence(IList<short> shorts, short[] sequence, int[] expectedIndices)
        {
            shorts.IndexOfSequences(sequence).ShouldBe(expectedIndices);
        }

        [Theory]
        [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 3, 2, 1 })]
        [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 2, 3, 3 })]
        [InlineData(new short[] { 1, 2, 3, 1, 2, 3, 2, 3 }, new short[] { 9 })]
        public void Returns_minus_one_if_sequence_not_found(IList<short> shorts, short[] sequence)
        {
            shorts.IndexOfSequences(sequence).ShouldBeEmpty();
        }
    }
}
