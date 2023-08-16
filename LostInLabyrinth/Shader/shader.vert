#version 330 core

layout (location = 0) in vec3 aPos;   // the position variable has attribute position 0
layout (location = 1) in vec2 aTex;   // the texture coordinate variable has attribute position 1
  
out vec2 texCoord; // output a color to the fragment shader

uniform mat4 Projection;
uniform mat4 Model;

void main()
{
    gl_Position = Projection * Model * vec4(aPos, 1.0);
    texCoord = aTex; // set texCoord to the input aTex texcoord we got from the vertex data
}      