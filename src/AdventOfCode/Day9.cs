using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Solver for Day 9
    /// </summary>
    public class Day9
    {
        public long Part1(string[] input)
        {
            string line = input[0];

            var files = new List<ChunkDescriptor>();
            var spaces = new List<ChunkDescriptor>();
            var disk = new List<int>();

            int pointer = 0;
            int fileId = 0;
            int spaceId = 0;

            // parse
            for (int i = 0; i < line.Length; i++)
            {
                int size = line[i] - '0';

                if (i % 2 == 0)
                {
                    for (int j = 0; j < size; j++)
                    {
                        disk.Add(fileId);
                        files.Add(new ChunkDescriptor(pointer, 1, fileId));
                        pointer++;
                    }

                    fileId++;
                }
                else if (size > 0)
                {
                    spaces.Add(new ChunkDescriptor(pointer, size, spaceId++));

                    for (int j = 0; j < size; j++)
                    {
                        disk.Add(-1);
                        pointer++;
                    }
                }
            }

            // shift
            foreach (ChunkDescriptor file in Enumerable.Reverse(files))
            {
                ChunkDescriptor space = spaces.FirstOrDefault(s => s.Pointer < file.Pointer);

                if (space == default)
                {
                    // no space left before the current file
                    break;
                }

                for (int i = 0; i < file.Size; i++)
                {
                    // move into position
                    disk[file.Pointer + i] = -1;
                    disk[space.Pointer + i] = file.Id;
                }

                // mark the space as filled
                space.Pointer += file.Size;
                space.Size -= file.Size;

                if (space.Size < 1)
                {
                    spaces.Remove(space);
                }
            }

            // checksum
            long total = 0;

            for (int i = 0; i < disk.Count; i++)
            {
                if (disk[i] < 0) break;

                total += i * disk[i];
            }

            return total;
        }

        public long Part2(string[] input)
        {
            string line = input[0];

            var files = new List<ChunkDescriptor>();
            var spaces = new List<ChunkDescriptor>();
            var disk = new List<int>();

            int pointer = 0;
            int fileId = 0;
            int spaceId = 0;

            // parse
            for (int i = 0; i < line.Length; i++)
            {
                int size = line[i] - '0';

                if (i % 2 == 0)
                {
                    files.Add(new ChunkDescriptor(pointer, size, fileId));

                    for (int j = 0; j < size; j++)
                    {
                        disk.Add(fileId);
                        pointer++;
                    }

                    fileId++;
                }
                else if (size > 0)
                {
                    spaces.Add(new ChunkDescriptor(pointer, size, spaceId++));

                    for (int j = 0; j < size; j++)
                    {
                        disk.Add(-1);
                        pointer++;
                    }
                }
            }

            // shift
            foreach (ChunkDescriptor file in Enumerable.Reverse(files))
            {
                ChunkDescriptor space = spaces.FirstOrDefault(s => s.Pointer < file.Pointer && s.Size >= file.Size);

                if (space == default)
                {
                    // doesn't fit anywhere
                    continue;
                }

                for (int i = 0; i < file.Size; i++)
                {
                    // move into position
                    disk[file.Pointer + i] = -1;
                    disk[space.Pointer + i] = file.Id;
                }

                // mark the space as filled
                space.Pointer += file.Size;
                space.Size -= file.Size;

                if (space.Size < 1)
                {
                    spaces.Remove(space);
                }
            }

            // checksum
            long total = 0;

            for (int i = 0; i < disk.Count; i++)
            {
                if (disk[i] < 0) continue; // blank spaces can be left in part 2 so keep going

                total += i * disk[i];
            }

            return total;
        }

        private class ChunkDescriptor
        {
            public int Pointer { get; set; }
            public int Size { get; set; }
            public int Id { get; }

            /// <summary>
            /// Initialises a new instance of the <see cref="ChunkDescriptor"/> class.
            /// </summary>
            public ChunkDescriptor(int pointer, int size, int id)
            {
                this.Pointer = pointer;
                this.Size = size;
                this.Id = id;
            }
        }
    }
}
