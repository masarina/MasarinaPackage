using UdonSharp;
using UnityEngine;

public class VarHolder : UdonSharpBehaviour
{
    public string strData;
    public float floatData;
    public float[][] floatArray2D;
    public float[][][] floatArray3D;
    public float[][][][] floatArray4D;
    public float[] floatArray;
    public int intData;
    public int[] intArray;
    public int[][] intArray2D;
    public int[][][] intArray3D;
    public int[][][][] intArray4D;


    // str型書き込みコード
    public void WriteStrData(string newData)
    {
        strData = newData;
    }
    
    // str型読み込みコード
    public string ReadStrData()
    {
        return strData;
    }
    
    
    // float型書き込みコード群
    public void WriteFloatData(float newData)
    {
        floatData = newData;
    }
    public void WriteFloatArray(float[] newData)
    {
        floatArray = newData;
    }
    public void WriteFloatArray2D(float[][] newData)
    {
        floatArray2D = newData;
    }
    public void WriteFloatArray3D(float[][][] newData)
    {
        floatArray3D = newData;
    }
    public void WriteFloatArray4D(float[][][][] newData)
    {
        floatArray4D = newData;
    }

    // float型書き込みコード群
    public float ReadFloatData()
    {
        return floatData;
    }
    public float[] ReadFloatArray()
    {
        return floatArray;
    }
    public float[][] ReadFloatArray2D()
    {
        return floatArray2D;
    }
    public float[][][] ReadFloatArray3D()
    {
        return floatArray3D;
    }
    public float[][][][] ReadFloatArray4D()
    {
        return floatArray4D;
    }
    
    
    // int型のデータを書き込むメソッド群
    public void WriteIntData(int newData)
    {
        intData = newData;
    }
    public void WriteIntArray(int[] newData)
    {
        intArray = newData;
    }
    public void WriteIntArray2D(int[][] newData)
    {
        intArray2D = newData;
    }
    public void WriteIntArray3D(int[][][] newData)
    {
        intArray3D = newData;
    }
    public void WriteIntArray4D(int[][][][] newData)
    {
        intArray4D = newData;
    }
    
    // int型のデータを読み込むメソッド群
    public int ReadIntData()
    {
        return intData;
    }
    public int[] ReadIntArray()
    {
        return intArray;
    }
    public int[][] ReadIntArray2D()
    {
        return intArray2D;
    }
    public int[][][] ReadIntArray3D()
    {
        return intArray3D;
    }
    public int[][][][] ReadIntArray4D()
    {
        return intArray4D;
    }


}