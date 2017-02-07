HexGrid
=======

A logical hex-grid implementation based on Amit Patel's examples at http://www.redblobgames.com/grids/hexagons/.

## Overview

This is a library designed to make computing hexagonal grid geometry straightforward and to enable a broad category of operations to get collections of hexes in various configurations surrounding or in relating to a given hex. Furthermore, it provides functionality to round a 2D point (i.e. cursor screen position or 2D world position) to the nearest hex.

This being said, this is a *logical* hex grid library. It does not actually lay hexes out in memory. It is purely a mathematical model to make that easier, should you need to do it. This, of course, means that this library will not consume large amounts of memory unless you use it to do that yourself.

## License

Licensed under the MIT License. See the LICENCE file for more information.
