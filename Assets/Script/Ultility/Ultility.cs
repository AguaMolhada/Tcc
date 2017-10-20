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
        System.Random rnd = new System.Random(seed.GetHashCode());

        for ( var i = 0 ; i < array.Length - 1 ; i++ ) {
            int randomIndex = rnd.Next(i , array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }
        return array;
    }

    public static void Shuffle<T>( T[,] array ) {
        System.Random rnd = new System.Random();
        int lengthRow = array.GetLength(1);

        for ( var i = array.Length - 1 ; i > 0 ; i-- ) {
            int i0 = i / lengthRow;
            int i1 = i % lengthRow;

            int j = rnd.Next(i + 1);
            int j0 = j / lengthRow;
            int j1 = j % lengthRow;

            T temp = array[i0 , i1];
            array[i0 , i1] = array[j0 , j1];
            array[j0 , j1] = temp;
        }
    }

    public static string GetRandomString( System.Random rnd , int length ) {
        int x = length;
        string charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%&*()[]{}<>,.;:/?";
        StringBuilder rs = new StringBuilder();

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
        int pattern = Random.Range(0 , 100);
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
        string[,] startName = new string[3 , 19] {{"B","C","D","F","G","H","J","K","L","M","N","P","R","S","T","V","W","X","Z"},
                                                 {"B","C","Ch","D","F","G","K","P","Ph","S","T","V","Z","R","L","","","",""},
                                                 {"Ch","St","Th","Ct","Ph","Qu","Squ","Sh","","","","","","","","","","",""}};
        a = Random.Range(0 , startName.GetLength(0));
        for ( int i = 0 ; i < 1 ; i++ ) {
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
        string[,] nameVowel = new string[2 , 22]{{"a","e","i","o","u","","","","","","","","","","","","","","","","",""},
            {"ao","ae","ai","au","ay","eo","ea","ei","ey","io","ia","iu","oa","oe","oi","ou","oy","ui","uo","uy","ee","oo"}};
        a = Random.Range(0 , nameVowel.GetLength(0));
        for ( int i = 0 ; i < 1 ; i++ ) {
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
        string[,] nameLink = new string[3 , 34] {{"b","c","d","f","g","h","j","k","l","m","n","p","r","s","t","v","w","x","z","","","","","","","","","","","","","","",""},
                                                {"b","c","ch","d","f","g","k","p","ph","r","s","t","v","z","r","l","n","","","","","","","","","","","","","","","","",""},
            {"ch","rt","rl","rs","rp","rb","rm","st","th","ct","ph","qu","tt","bb","nn","mm","gg","cc","dd","ff","pp","rr","ll","vv","ww","ck","squ","lm","sh","wm","wb","wt","lb","rg"}};
        a = Random.Range(0 , nameLink.GetLength(0));
        for ( int i = 0 ; i < 1 ; i++ ) {
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
        string[] nameEnd = new string[42] { "id" , "ant" , "on" , "ion" , "an" , "in" , "at" , "ate" , "us" , "oid" , "aid" , "al" , "ark" , "ork" , "irk" , "as" , "os" , "e" , "o" , "a" , "y" , "or" , "ore" , "es" , "ot" , "at" , "ape" , "ope" , "el" , "er" , "ex" , "ox" , "ax" , "ie" , "eep" , "ap" , "op" , "oop" , "aut" , "ond" , "ont" , "oth" };
        a = Random.Range(0 , nameEnd.Length);
        return nameEnd[a];
    }
    #endregion

    #region City Name Generator
    public static string CityNameGenerator() {
        int pattern = Random.Range(0 , 100);
        if ( pattern >= 0 && pattern < 20 ) { return StartName() + CityNameEnd(); }
        else if ( pattern >= 20 && pattern < 40 ) { return NameVowel() + NameLink() + CityNameEnd(); }
        else if ( pattern >= 40 && pattern < 60 ) { return StartName() + NameVowel() + NameLink() + CityNameEnd(); }
        else if ( pattern >= 60 && pattern < 80 ) { return NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd(); }
        else if ( pattern >= 80 && pattern < 100 ) { return StartName() + NameVowel() + NameLink() + NameVowel() + NameLink() + CityNameEnd(); }
        return null;
    }
    static string CityNameEnd() {
        int a;
        string[] nameEnd = new string[] { "ville" , "polis" , " City" , " Village" , "town" , "port" , "boro" , "burg" , "burgh" , "carden" , "field" , "ness" };
        a = Random.Range(0 , nameEnd.Length);
        return nameEnd[a];
    }
    #endregion
}

public static class Noise
{

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves,float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
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

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
    public static float[,] GenerateNoiseMap( int mapChunk, int seed , float scale , int octaves , float persistance , float lacunarity , Vector2 offset ) {
        float[,] noiseMap = new float[mapChunk , mapChunk];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for ( int i = 0 ; i < octaves ; i++ ) {
            float offsetX = prng.Next(-100000 , 100000) + offset.x;
            float offsetY = prng.Next(-100000 , 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX , offsetY);
        }

        if ( scale <= 0 ) {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfChuk = mapChunk / 2f;
        
        for ( int y = 0 ; y < mapChunk ; y++ ) {
            for ( int x = 0 ; x < mapChunk ; x++ ) {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for ( int i = 0 ; i < octaves ; i++ ) {
                    float sampleX = (x - halfChuk) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfChuk) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX , sampleY) * 2 - 1;
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

        for ( int y = 0 ; y < halfChuk ; y++ ) {
            for ( int x = 0 ; x < mapChunk ; x++ ) {
                noiseMap[x , y] = Mathf.InverseLerp(minNoiseHeight , maxNoiseHeight , noiseMap[x , y]);
            }
        }

        return noiseMap;
    }

}

public static class FalloffGenerator
{
    public static float[,] GenerateFalloffMap(int size)
    {
        float[,] map = new float[size,size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float) size * 2 - 1;
                float y = j / (float) size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i, j] = Evaluate(value);
            }
        }
        return map;
    }

    private static float Evaluate(float value)
    {
        float a = 5;
        float b = 3.45f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}

public static class BrownianEffectGenerator {
    public static float ConsBoltzmann = 1.3806503e-23f;
    public static float Temperature = 297.55f;
    public static float Viscosidade = 891.0f;
    public static float ParticleRay = 0.5f;

    public static float[,] BrownianEffectMap( int size ) {
        float[,] map = new float[size , size];
        int t = 0;
        for ( int x = 0 ; x < size ; x++ ) {
            for ( int y = 0 ; y < size ; y++ ) {
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
        Texture2D texture = new Texture2D(width,height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(cm);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] hm)
    {
        int width = hm.GetLength(0);
        int height = hm.GetLength(1);

        Color[] colourMap = new Color[width * height];
        for ( int y = 0 ; y < height ; y++ ) {
            for ( int x = 0 ; x < width ; x++ ) {
                colourMap[y * width + x] = Color.Lerp(Color.black , Color.white , hm[x , y]);
            }
        }

        return TextureFromColourMap(colourMap , width , height);
    }

}

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap,float heightMultiplier,AnimationCurve heightInfluenceCurve,int levelOfDetail,bool useFlatShading)
    {
        int w = heightMap.GetLength(0);
        int h = heightMap.GetLength(1);
        float topLeftX = (w - 1) / -2f;
        float topleftZ = (h - 1) / 2f;

        int meshSimplificationIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2;
        int verticesPerLine = (w - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < h; y+= meshSimplificationIncrement )
        {
            for (int x = 0; x < w; x+= meshSimplificationIncrement )
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

    public static MeshData GenerateTerrainMesh(float[,] heightMap,int levelOfDetail)
    {
        int w = heightMap.GetLength(0);
        int h = heightMap.GetLength(1);
        float topLeftX = (w - 1) / -2f;
        float topleftZ = (h - 1) / 2f;

        int meshSimplificationIncrement = 1;
        int verticesPerLine = (w - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < h; y += meshSimplificationIncrement)
        {
            for (int x = 0; x < w; x += meshSimplificationIncrement)
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
