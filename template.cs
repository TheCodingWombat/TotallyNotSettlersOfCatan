using System;
using System.IO;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace TotallyNotSettlersOfCatan {
	public class OpenTKApp : GameWindow
	{
		static int screenID;
		static Game game;
		static bool terminated = false;
        static float zoom = 1;

        public static float Zoom { get => zoom; set => zoom = value; }

        protected override void OnLoad( EventArgs e )
		{
			// called upon app init
			
			GL.Hint( HintTarget.PerspectiveCorrectionHint, HintMode.Nicest );
            
			ClientSize = new Size( 640, 400 );
            Location = new Point(0, 0);
			game = new Game(this);
			game.screen = new Surface( Width, Height );
			Sprite.target = game.screen;
			screenID = game.screen.GenTexture();
			game.Init();

            MouseMove += Mouse_Move;
            MouseDown += Mouse_Down;

		}

        public PointF ScreenToGameCoords (Point p) {
            float x = (p.X - ClientSize.Width / 2f) / (ClientSize.Width / (2f * zoom));
            float y = -(p.Y - ClientSize.Height / 2f) / (ClientSize.Width / (2f * zoom));

            return new PointF(x, y);
        }

        public void Mouse_Move(object sender, MouseMoveEventArgs e) {
            game.Mouse_Move(ScreenToGameCoords(e.Position));
        }

        public void Mouse_Down(object sender, MouseButtonEventArgs e) {
            game.Mouse_Down(ScreenToGameCoords(e.Position), e.Button);
        }

        protected override void OnUnload( EventArgs e )
		{
			// called upon app close
			GL.DeleteTextures( 1, ref screenID );
			Environment.Exit( 0 ); // bypass wait for key on CTRL-F5
		}
		protected override void OnResize( EventArgs e )
		{
			// called upon window resize
			GL.Viewport(0, 0, Width, Height);
			GL.MatrixMode( MatrixMode.Projection );
			GL.LoadIdentity();
            GL.Ortho(-1.0 * Zoom, 1.0 * Zoom, (float)Height / Width * -1.0f * Zoom, (float)Height / Width * 1.0f * Zoom, 0.0, 4.0);
        }
		protected override void OnUpdateFrame( FrameEventArgs e )
		{
			// called once per frame; app logic
			var keyboard = OpenTK.Input.Keyboard.GetState();
			if (keyboard[OpenTK.Input.Key.Escape]) this.Exit();
		}
		protected override void OnRenderFrame( FrameEventArgs e )
		{
			// called once per frame; render
			game.Tick();
			if (terminated) 
			{
				Exit();
				return;
			}

            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest);
            GL.Color3(1.0f, 1.0f, 1.0f);

            // convert Game.screen to OpenGL texture
            GL.BindTexture( TextureTarget.Texture2D, screenID );
			GL.TexImage2D( TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 
						   game.screen.width, game.screen.height, 0, 
						   OpenTK.Graphics.OpenGL.PixelFormat.Bgra, 
						   PixelType.UnsignedByte, game.screen.pixels 
						 );
			// clear window contents
			GL.Clear( ClearBufferMask.ColorBufferBit );
			// setup camera
			GL.MatrixMode( MatrixMode.Modelview );
			GL.LoadIdentity();


            //Disabled this since we want to use ortho

			//GL.MatrixMode( MatrixMode.Projection );
			//GL.LoadIdentity();


			// draw screen filling quad
			GL.Begin( PrimitiveType.Quads );
			GL.TexCoord2( 0.0f, 1.0f ); GL.Vertex2( -1.0f, -1.0f );
			GL.TexCoord2( 1.0f, 1.0f ); GL.Vertex2(  1.0f, -1.0f );
			GL.TexCoord2( 1.0f, 0.0f ); GL.Vertex2(  1.0f,  1.0f );
			GL.TexCoord2( 0.0f, 0.0f ); GL.Vertex2( -1.0f,  1.0f );
			GL.End();
            // tell OpenTK we're done rendering

            // prepare for generic OpenGL rendering
            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Texture2D);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            
            game.RenderGL();

			SwapBuffers();
		}
		public static void Main( string[] args ) 
		{ 
			// entry point
			using (OpenTKApp app = new OpenTKApp()) { app.Run( 30.0, 0.0 ); }
		}
	}
}