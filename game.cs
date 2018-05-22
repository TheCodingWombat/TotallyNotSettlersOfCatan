using System;
using System.IO;

namespace TotallyNotSettlersOfCatan {

class Game
    {
	    // member variables
	    public Surface screen;
	    // initialize
	    public void Init()
	    {
	    }
	    // tick: renders one frame
	    public void Tick()
	    {

		    screen.Clear( 0 );
		    screen.Print( "hello world", 2, 2, 0xffffff );
            screen.Line(2, 20, 160, 20, 0xff0000);

            screen.Line(25, (int)Math.Floor(50 * Math.Sqrt(3) / 2), 75, (int) Math.Floor(50 * Math.Sqrt(3) / 2), 0xff0000);
            screen.Line(75, (int)Math.Floor(50 * Math.Sqrt(3) / 2), 100, (int)Math.Floor(100 * Math.Sqrt(3) / 2), 0xff0000);
            screen.Line(100, (int)Math.Floor(100 * Math.Sqrt(3) / 2), 75, (int)Math.Floor(150 * Math.Sqrt(3) / 2), 0xff0000);
            screen.Line(75, (int)Math.Floor(150 * Math.Sqrt(3) / 2), 25, (int)Math.Floor(150 * Math.Sqrt(3) / 2), 0xff0000);
            screen.Line(25, (int)Math.Floor(150 * Math.Sqrt(3) / 2), 0, (int)Math.Floor(100 * Math.Sqrt(3) / 2), 0xff0000);
            screen.Line(0, (int)Math.Floor(100 * Math.Sqrt(3) / 2), 25, (int)Math.Floor(50 * Math.Sqrt(3) / 2), 0xff0000);
        }
    }

} // namespace Template