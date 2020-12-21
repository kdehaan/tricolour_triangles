## General Overview

The general premise of this problem is to fill in the interior dots on a polygon (or 'quilt') with triangles such that there is no more than a given number of 'completed' (that is, comprising of all three colours) triangles.

The plain english description for how this program works is that it identifies two-value 'paired' edges, such as a-b, a-c, or b-c, and then determines if it can make them go away. In the event that a paired edge is palindromic (for example, a-b-a or c-a-a-a-a-a-c), it attempts to 'cover' the middle of the palindrome by colouring over it with the value on the extremes of the sequence. The active border of the polygon is then updated and the search for palindromes is performed again until the low hanging fruit is gone.

Note to self:

Read Clean Code by Robert C Martin
