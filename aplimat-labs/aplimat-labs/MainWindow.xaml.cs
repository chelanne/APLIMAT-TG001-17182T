using SharpGL;
using SharpGL.SceneGraph.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using aplimat_labs.Models;
using aplimat_labs.Utilities;

namespace aplimat_labs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private CubeMesh lightCube = new CubeMesh()
        {
            Position = new Vector3(-25, 13, 0),
            Mass = 2
        };

        private CubeMesh mediumCube = new CubeMesh()
        {
            Position = new Vector3(-20, 13, 0),
            Mass = 4
        };

        private CubeMesh heavyCube = new CubeMesh()
        {
            Position = new Vector3(-10, 13, 0),
            Mass = 6
        };

        // x +/-28
        // y +/-16

        private Vector3 wind = new Vector3(0.3f, 0, 0);
        private Vector3 gravity = new Vector3(0, -.3f, 0);

        private Vector3 floor = new Vector3(0, 0.03f, 0);

        private float rightBorder = 27;
        private float bottomBorder = -14;

        private void OpenGLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // Move Left And Into The Screen
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -40.0f);

            lightCube.Draw(gl,1,0,0);
            lightCube.ApplyForce(wind);
            lightCube.ApplyForce(gravity);

            mediumCube.Draw(gl,0,1,0);
            mediumCube.ApplyForce(wind);
            mediumCube.ApplyForce(gravity);

            heavyCube.Draw(gl,0,0,1);
            heavyCube.ApplyForce(wind);
            heavyCube.ApplyForce(gravity);

            if (heavyCube.Position.x >= rightBorder)
            {
                heavyCube.Velocity.x *= -1;
            }
            if (mediumCube.Position.x >= rightBorder)
            {
                mediumCube.Velocity.x *= -1;
            }
            if (lightCube.Position.x >= rightBorder)
            {
                lightCube.Velocity.x *= -1;
            }

            if (heavyCube.Position.y <= bottomBorder)
            {
                heavyCube.Velocity.y *= -1;
             }
            if (mediumCube.Position.y <= bottomBorder)
            {
                mediumCube.Velocity.y *= -1;
            }
            if (lightCube.Position.y <= bottomBorder)
            {
                lightCube.Velocity.y *= -1;
            }
        }




        private void OpenGLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            gl.Enable(OpenGL.GL_DEPTH_TEST);

            float[] global_ambient = new float[] { 0.5f, 0.5f, 0.5f, 1.0f };
            float[] light0pos = new float[] { 0.0f, 5.0f, 10.0f, 1.0f };
            float[] light0ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light0diffuse = new float[] { 0.3f, 0.3f, 0.3f, 1.0f };
            float[] light0specular = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };

            float[] lmodel_ambient = new float[] { 0.2f, 0.2f, 0.2f, 1.0f };
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, lmodel_ambient);

            gl.LightModel(OpenGL.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, light0pos);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, light0ambient);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, light0diffuse);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_SPECULAR, light0specular);
            gl.Disable(OpenGL.GL_LIGHTING);
            gl.Disable(OpenGL.GL_LIGHT0);

            gl.ShadeModel(OpenGL.GL_SMOOTH);
        }
    }
}
