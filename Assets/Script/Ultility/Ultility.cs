// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ultility.cs" company="Dauler Palhares">
//  © Copyright Dauler Palhares da Costa Viana 2017.
//          http://github.com/DaulerPalhares
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
// This Class have all the static classes used on the project

using System;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Ultility {

    public static Vector3 DivVector3( Vector3 first , Vector3 second ) {
        return new Vector3((first.x / second.x) , (first.y / second.y) , (first.z / second.z));
    }

    public static T[] ShuffleArray<T>( T[] array , string seed ) {
        var rnd = new System.Random(seed.GetHashCode());

        for ( var i = 0 ; i < array.Length - 1 ; i++ ) {
            var randomIndex = rnd.Next(i , array.Length);
            var tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static void Shuffle<T>( T[,] array ) {
        var rnd = new System.Random();
        var lengthRow = array.GetLength(1);

        for ( var i = array.Length - 1 ; i > 0 ; i-- ) {
            var i0 = i / lengthRow;
            var i1 = i % lengthRow;

            var j = rnd.Next(i + 1);
            var j0 = j / lengthRow;
            var j1 = j % lengthRow;

            var temp = array[i0 , i1];
            array[i0 , i1] = array[j0 , j1];
            array[j0 , j1] = temp;
        }
    }

    public static string GetRandomString( System.Random rnd , int length ) {
        var x = length;
        var charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%&*()[]{}<>,.;:/?";
        var rs = new StringBuilder();

        while ( x != 0 ) {
            rs.Append(charPool[(int)(rnd.NextDouble() * charPool.Length)]);
            x--;
        }
        return rs.ToString();
    }

    public static float GetPercent( float total , float percent ) {
        return (percent / 100) * total;
    }

    public static float PercentValue( float total , float percent ) {
        return (percent / total) * 100;
    }

    #region Name Generator
    public static string NameGenerator() {
        var pattern = Random.Range(0 , 100);
        if ( pattern >= 0 && pattern < 40 ) { return StartName() + NameEnd(); }
        else if ( pattern >= 40 && pattern < 50 ) { return NameVowel() + NameLink() + NameEnd(); }
        else if ( pattern >= 50 && pattern < 60 ) { return StartName() + NameVowel() + NameLink() + NameEnd(); }
        else if ( pattern >= 60 && pattern < 70 ) { return NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd(); }
        else if ( pattern >= 70 && pattern < 80 ) { return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd(); }
        else if ( pattern >= 80 && pattern < 90 ) { return NameVowel() + NameLink() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd(); }
        else if ( pattern >= 90 && pattern < 100 ) { return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameVowel() + NameLink() + NameEnd(); }
        return null;
    }

    static string StartName() {
        int a, b;
        var startName = new string[3 , 19] {{"B","C","D","F","G","H","J","K","L","M","N","P","R","S","T","V","W","X","Z"},
                                                 {"B","C","Ch","D","F","G","K","P","Ph","S","T","V","Z","R","L","","","",""},
                                                 {"Ch","St","Th","Ct","Ph","Qu","Squ","Sh","","","","","","","","","","",""}};
        a = Random.Range(0 , startName.GetLength(0));
        for ( var i = 0 ; i < 1 ; i++ ) {
            b = Random.Range(0 , startName.GetLength(1));
            if ( startName[a , b] == "" ) {
                i--;
            }
            else {
                return startName[a , b];
            }
        }
        return null;
    }

    static string NameVowel() {
        int a, b;
        var nameVowel = new string[2 , 22]{{"a","e","i","o","u","","","","","","","","","","","","","","","","",""},
            {"ao","ae","ai","au","ay","eo","ea","ei","ey","io","ia","iu","oa","oe","oi","ou","oy","ui","uo","uy","ee","oo"}};
        a = Random.Range(0 , nameVowel.GetLength(0));
        for ( var i = 0 ; i < 1 ; i++ ) {
            b = Random.Range(0 , nameVowel.GetLength(1));
            if ( nameVowel[a , b] == "" ) {
                i--;
            }
            else {
                return nameVowel[a , b];
            }
        }
        return null;
    }

    static string NameLink() {
        int a, b;
        var nameLink = new string[3 , 34] {{"b","c","d","f","g","h","j","k","l","m","n","p","r","s","t","v","w","x","z","","","","","","","","","","","","","","",""},
                                                {"b","c","ch","d","f","g","k","p","ph","r","s","t","v","z","r","l","n","","","","","","","","","","","","","","","","",""},
            {"ch","rt","rl","rs","rp","rb","rm","st","th","ct","ph","qu","tt","bb","nn","mm","gg","cc","dd","ff","pp","rr","ll","vv","ww","ck","squ","lm","sh","wm","wb","wt","lb","rg"}};
        a = Random.Range(0 , nameLink.GetLength(0));
        for ( var i = 0 ; i < 1 ; i++ ) {
            b = Random.Range(0 , nameLink.GetLength(1));
            if ( nameLink[a , b] == "" ) {
                i--;
            }
            else {
                return nameLink[a , b];
            }
        }
        return null;
    }

    static string NameEnd() {
        int a;
        var nameEnd = new string[42] { "id" , "ant" , "on" , "ion" , "an" , "in" , "at" , "ate" , "us" , "oid" , "aid" , "al" , "ark" , "ork" , "irk" , "as" , "os" , "e" , "o" , "a" , "y" , "or" , "ore" , "es" , "ot" , "at" , "ape" , "ope" , "el" , "er" , "ex" , "ox" , "ax" , "ie" , "eep" , "ap" , "op" , "oop" , "aut" , "ond" , "ont" , "oth" };
        a = Random.Range(0 , nameEnd.Length);
        return nameEnd[a];
    }
    #endregion

    #region City Name Generator
    public static string CityNameGenerator() {
        var pattern = Random.Range(0 , 100);
        if ( pattern >= 0 && pattern < 20 ) { return StartName() + CityNameEnd(); }
        else if ( pattern >= 20 && pattern < 40 ) { return NameVowel() + NameLink() + CityNameEnd(); }
        else if ( pattern >= 40 && pattern < 60 ) { return StartName() + NameVowel() + NameLink() + CityNameEnd(); }
        else if ( pattern >= 60 && pattern < 80 ) { return NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd(); }
        else if ( pattern >= 80 && pattern < 100 ) { return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd(); }
        return null;
    }
    static string CityNameEnd() {
        int a;
        var nameEnd = new string[] { "ville" , "polis" , " City" , " Village" , "town" , "port" , "boro" , "burg" , "burgh" , "garden" , "field" , "ness" };
        a = Random.Range(0 , nameEnd.Length);
        return nameEnd[a];
    }
    #endregion
}
/// <summary>
/// Static Class that handle the perlin noise generator
/// </summary>
public static class Noise
{
    /// <summary>
    /// Class that will generate a Perlin Noise
    /// </summary>
    /// <param name="mapWidth">Noise Map Width</param>
    /// <param name="mapHeight">Noise Map Height</param>
    /// <param name="seed">Seed used in the random</param>
    /// <param name="scale">Noise Zoom</param>
    /// <param name="octaves">Number of octaves</param>
    /// <param name="persistance">Persistence Value</param>
    /// <param name="lacunarity">Lacunarity Value</param>
    /// <param name="offset">Noise offset</param>
    /// <returns>Return a 2d Float array od noise</returns>
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves,float persistance, float lacunarity, Vector2 offset)
    {
        var noiseMap = new float[mapWidth, mapHeight];

        var prng = new System.Random(seed);
        var octaveOffsets = new Vector2[octaves];
        for (var i = 0; i < octaves; i++)
        {
            var offsetX = prng.Next(-100000, 100000) + offset.x;
            var offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        var maxNoiseHeight = float.MinValue;
        var minNoiseHeight = float.MaxValue;

        var halfWidth = mapWidth / 2f;
        var halfHeight = mapHeight / 2f;

        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (var i = 0; i < octaves; i++)
                {
                    var sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    var sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    var perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (var y = 0; y < mapHeight; y++)
        {
            for (var x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
    /// <summary>
    /// Class that will generate a Perlin Noise
    /// </summary>
    /// <param name="mapChunk">Map Chunck Size</param>
    /// <param name="seed">Seed used in the random</param>
    /// <param name="scale">Noise Zoom</param>
    /// <param name="octaves">Number of octaves</param>
    /// <param name="persistance">Persistence Value</param>
    /// <param name="lacunarity">Lacunarity Value</param>
    /// <param name="offset">Noise offset</param>
    /// <returns>Return a 2d Float array od noise</returns>
    public static float[,] GenerateNoiseMap( int mapChunk, int seed , float scale , int octaves , float persistance , float lacunarity , Vector2 offset ) {
        var noiseMap = new float[mapChunk , mapChunk];

        var prng = new System.Random(seed);
        var octaveOffsets = new Vector2[octaves];
        for ( var i = 0 ; i < octaves ; i++ ) {
            var offsetX = prng.Next(-100000 , 100000) + offset.x;
            var offsetY = prng.Next(-100000 , 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX , offsetY);
        }

        if ( scale <= 0 ) {
            scale = 0.0001f;
        }

        var maxNoiseHeight = float.MinValue;
        var minNoiseHeight = float.MaxValue;

        var halfChuk = mapChunk / 2f;
        
        for ( var y = 0 ; y < mapChunk ; y++ ) {
            for ( var x = 0 ; x < mapChunk ; x++ ) {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for ( var i = 0 ; i < octaves ; i++ ) {
                    var sampleX = (x - halfChuk) / scale * frequency + octaveOffsets[i].x;
                    var sampleY = (y - halfChuk) / scale * frequency + octaveOffsets[i].y;

                    var perlinValue = Mathf.PerlinNoise(sampleX , sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if ( noiseHeight > maxNoiseHeight ) {
                    maxNoiseHeight = noiseHeight;
                }
                else if ( noiseHeight < minNoiseHeight ) {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x , y] = noiseHeight;
            }
        }

        for ( var y = 0 ; y < halfChuk ; y++ ) {
            for ( var x = 0 ; x < mapChunk ; x++ ) {
                noiseMap[x , y] = Mathf.InverseLerp(minNoiseHeight , maxNoiseHeight , noiseMap[x , y]);
            }
        }

        return noiseMap;
    }
}
/// <summary>
/// Static Class that handle the falloff effect on map
/// </summary>
public static class FalloffGenerator
{
    /// <summary>
    /// Method to create a falloff effect
    /// </summary>
    /// <param name="size">Size of the falloff effect</param>
    /// <returns>A 2D float array with the effect</returns>
    public static float[,] GenerateFalloffMap(int size)
    {
        var map = new float[size,size];

        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                var x = i / (float) size * 2 - 1;
                var y = j / (float) size * 2 - 1;

                var value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i, j] = Evaluate(value);
            }
        }
        return map;
    }
    /// <summary>
    /// Function to create the effect
    /// </summary>
    /// <param name="value">value passed to the function</param>
    /// <returns>Function value</returns>
    private static float Evaluate(float value)
    {
        float a = 5;
        var b = 3.45f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}

public static class BrownianEffectGenerator {
    public static float ConsBoltzmann = 1.3806503e-23f;
    public static float Temperature = 297.55f;
    public static float Viscosidade = 891.0f;
    public static float ParticleRay = 0.5f;

    public static float[,] BrownianEffectMap( int size ) {
        var map = new float[size , size];
        var t = 0;
        for ( var x = 0 ; x < size ; x++ ) {
            for ( var y = 0 ; y < size ; y++ ) {
                map[x , y] = ((4 * ConsBoltzmann * Temperature) / (6 * Mathf.PI * Viscosidade * ParticleRay)) * t;
                t++;
            }
        }

        return map;
    }
}

public static class TextureGenerator
{
    public static Texture2D TextureFromColourMap(Color[] cm, int width, int height)
    {
        var texture = new Texture2D(width,height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(cm);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] hm)
    {
        var width = hm.GetLength(0);
        var height = hm.GetLength(1);

        var colourMap = new Color[width * height];
        for ( var y = 0 ; y < height ; y++ ) {
            for ( var x = 0 ; x < width ; x++ ) {
                colourMap[y * width + x] = Color.Lerp(Color.black , Color.white , hm[x , y]);
            }
        }

        return TextureFromColourMap(colourMap , width , height);
    }

}
/// <summary>
/// Class used to generate the mesh in real time
/// </summary>
public static class MeshGenerator
{
    /// <summary>
    /// Generate a mesh
    /// </summary>
    /// <param name="heightMap">Map used to pass the terrain height</param>
    /// <param name="heightMultiplier">Float used to Multiplier the height map value and make things more hight</param>
    /// <param name="heightInfluenceCurve">Ammount of influence that the height will have in the end</param>
    /// <param name="levelOfDetail">Level of detail of the mesh</param>
    /// <param name="useFlatShading">Will use flat shadding</param>
    /// <returns>A mesh created with all value</returns>
    public static MeshData GenerateTerrainMesh(float[,] heightMap,float heightMultiplier,AnimationCurve heightInfluenceCurve,int levelOfDetail,bool useFlatShading)
    {
        var w = heightMap.GetLength(0);
        var h = heightMap.GetLength(1);
        var topLeftX = (w - 1) / -2f;
        var topleftZ = (h - 1) / 2f;

        var meshSimplificationIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2;
        var verticesPerLine = (w - 1) / meshSimplificationIncrement + 1;

        var meshData = new MeshData(verticesPerLine, verticesPerLine);
        var vertexIndex = 0;

        for (var y = 0; y < h; y+= meshSimplificationIncrement )
        {
            for (var x = 0; x < w; x+= meshSimplificationIncrement )
            {
                meshData.Vertices[vertexIndex] = new Vector3(topLeftX + x, heightInfluenceCurve.Evaluate(heightMap[x, y])*heightMultiplier, topleftZ - y);
                meshData.Uvs[vertexIndex] = new Vector2(x / (float) w, y / (float) h);
                if (x < w - 1 && y < h - 1)
                {
                    meshData.AddTriangle(vertexIndex , vertexIndex + verticesPerLine + 1 , vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex + verticesPerLine + 1 , vertexIndex , vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        if (useFlatShading)
        {
            meshData.FlatShading();
        }
        return meshData;
    }
    /// <summary>
    /// Generate a mesh
    /// </summary>
    /// <param name="heightMap">Map used to pass the terrain height</param>
    /// <param name="levelOfDetail">Level of detail of the mesh</param>
    /// <returns>A mesh created with all value</returns>
    public static MeshData GenerateTerrainMesh(float[,] heightMap,int levelOfDetail)
    {
        var w = heightMap.GetLength(0);
        var h = heightMap.GetLength(1);
        var topLeftX = (w - 1) / -2f;
        var topleftZ = (h - 1) / 2f;

        var meshSimplificationIncrement = 1;
        var verticesPerLine = (w - 1) / meshSimplificationIncrement + 1;

        var meshData = new MeshData(verticesPerLine, verticesPerLine);
        var vertexIndex = 0;

        for (var y = 0; y < h; y += meshSimplificationIncrement)
        {
            for (var x = 0; x < w; x += meshSimplificationIncrement)
            {
                if (heightMap[x, y] < 0.46f)
                {
                    meshData.Vertices[vertexIndex] = new Vector3(topLeftX + x, 0, topleftZ - y);
                    meshData.Uvs[vertexIndex] = new Vector2(x / (float)w, y / (float)h);
                    if (x < w - 1 && y < h - 1)
                    {
                        meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                        meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                    }

                }
                else
                {
                    meshData.Vertices[vertexIndex] = new Vector3(topLeftX + x-1, 0, topleftZ - y-1);
                    meshData.Uvs[vertexIndex] = new Vector2(x-1 / (float)w, y-1 / (float)h);
                }
                vertexIndex++;
            }
        }
        return meshData;
    }

}

/// <summary>
/// Static Class that handle all things related to the building grid
/// </summary>
public static class BuildingGrid
{
    /// <summary>
    /// Generate a new building grid with empty (0) or null (-1) spaces
    /// </summary>
    /// <param name="gridSize">Size of the building grid.</param>
    /// <param name="noiseMap">Noise map used to generate the map. Used to check the grid space</param>
    /// <returns>2D int array with 0 or -1</returns>
    public static int[,] GenerateBuildingGrid(int gridSize, float[,] noiseMap)
    {
        var mapBuildingGrid = new int[gridSize, gridSize];
        var noiseMapX = 0;
        var noiseMapY = 0;

        for ( var y = 0 ; y < gridSize ; y++ ) {
            if (y> 0 && y % 10 == 0)
            {
                noiseMapY++;
            }
            if(y == 0)
            {
                noiseMapY = 0;
            }
            for ( var x = 0 ; x < gridSize ; x++ ) {
                if (x >0 && x % 10 == 0)
                {
                    noiseMapX++;
                }
                if(x == 0)
                {
                    noiseMapX = 0;
                }
                if (noiseMap[noiseMapX, noiseMapY] >= 0.46)
                {
                    mapBuildingGrid[x, y] = 0;
                }
                else
                {
                    mapBuildingGrid[x, y] = -1;
                }
            }
            noiseMapX = 0;
        }

        return mapBuildingGrid;
    }
    /// <summary>
    /// Get the relative position in the grid related to the world.
    /// </summary>
    /// <param name="grid">Grid to check the position</param>
    /// <param name="x">Grid position in X</param>
    /// <param name="y">Grid position in Y</param>
    /// <returns>World position (XWorld,0,YWorld)</returns>
    public static Vector3 GridPositionRelatedToWorld(int[,] grid,int x, int y)
    {
        var topLeftX = (grid.GetLength(0) - 1) / -2f;
        var topleftZ = (grid.GetLength(1) - 1) / 2f;

        return new Vector3(topLeftX + (x / 10f) + 0.05f , 0 , topleftZ - (y / 10f) - 0.05f);
    }

}