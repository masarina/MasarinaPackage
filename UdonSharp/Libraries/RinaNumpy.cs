/// version_0.0.0
/// this file made by Masarina
/// meamメソッドはMean_FloatArrayに名前を変更しました(2024-11-15)
using UdonSharp;
using UnityEngine;

public class RinaNumpy : UdonSharpBehaviour
{
    //
    public static float Sum_FloatArray(float[] x) {
        float sum = 0;
        foreach (float value in x) {
            sum += value;
        }
        return sum;
    }
    
    //
    public static float[] Divide_FloatArray_Float(float[] x, float y) {
        float epsilon = 1e-6f; // ゼロ除算を避けるための小さな値
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] / (y + epsilon); // イプシロンを追加してゼロ除算を避ける
        }
        return result;
    }
    
    //
    public static float[] Subtract_FloatArray_Float(float[] x, float y) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] - y;
        }
        return result;
    }
    
    public static float[] Multiply_FloatArray_FloatArray(float[] x, float[] y) {
        if (x.Length != y.Length) {
            Debug.LogError("Arrays must be of equal length.");
            return new float[0]; // 空の配列を返すことで処理を安全に終了
        }
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] * y[i];
        }
        return result;
    }

    
    //
    public static float[] Multiply_FloatArray_Float(float[] x, float y) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] * y;
        }
        return result;
    }
    
    //
    public static float[] Add_FloatArray_FloatArray(float[] x, float[] y) {
        if (x.Length != y.Length) {
            Debug.LogError("Arrays must be of equal length.");
            return new float[0]; // 空の配列を返す
        }
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = x[i] + y[i];
        }
        return result;
    }

    
    //
    public static float[] OnesLike_FloatArray(float[] x) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = 1;
        }
        return result;
    }
    
    //
    public static float[] ZerosLike_FloatArray(float[] x) {
        return new float[x.Length]; // Default initialization to 0
    }
    

    //
    public static float[] Negative_FloatArray(float[] x) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = -x[i];
        }
        return result;
    }
    
    //
    public static float DotProduct_FloatArray_FloatArray(float[] x, float[] y) {
        if (x.Length != y.Length) {
            Debug.LogError("Arrays must be of equal length for dot product.");
            return 0; // エラー時は0を返す
        }
        float result = 0;
        for (int i = 0; i < x.Length; i++) {
            result += x[i] * y[i];
        }
        return result;
    }

    //
    public static float[] DotProduct_FloatArray2D_FloatArray(float[][] A, float[] x) {
        if (A.Length == 0 || A[0].Length != x.Length) {
            Debug.LogError("Matrix columns and vector size must match for dot product.");
            return new float[0];
        }
    
        float[] result = new float[A.Length];
        for (int i = 0; i < A.Length; i++) {
            for (int j = 0; j < x.Length; j++) {
                result[i] += A[i][j] * x[j];
            }
        }
        return result;
    }
    
    //
    public static float Mean_FloatArray(float[] x) 
    {
        float sum = 0; // 全要素の合計値を保持する変数
        foreach (float value in x) 
        {
            sum += value; // 配列xの各要素を合計に加える
        }
        return sum / x.Length; // 合計を要素数で割って平均値を求める
    }
    

    
    public static float[] Power_FloatArray_Float(float[] x, float y) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = Mathf.Pow(x[i], y);
        }
        return result;
    }
    
    public static float[] Sum_FloatArray2d_Float_axis0(float[][] x)
    {
        float[] sum = new float[x[0].Length];
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j < x[i].Length; j++)
            {
                sum[j] += x[i][j];
            }
        }
        return sum;
    }
    
    public static float[] Sum_FloatArray2d_FloatArray2d_axis1(float[][] x)
    {
        float[] sum = new float[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j < x[i].Length; j++)
            {
                sum[i] += x[i][j];
            }
        }
        return sum;
    }
    
    // 分散を計算するためのヘルパーメソッド
    public float Var_FloatArray(float[] x, float mean)
    {
        float sumOfSquares = 0f;
        foreach (float value in x)
        {
            sumOfSquares += Mathf.Pow(value - mean, 2);
        }
        return sumOfSquares / x.Length;
    }
    
    /// <summary>
    /// 整数を指定したビット数の2進数文字列に変換します。
    /// </summary>
    /// <param name="value">変換する整数値</param>
    /// <param name="bitCount">指定するビット数</param>
    /// <returns>指定ビット数の2進数文字列</returns>
    public string ConvertToBinary(int value, int bitCount)
    {
        string binaryString = "";  // 変換結果の2進数文字列を保持する変数
        for (int i = 0; i < bitCount; i++)
        {
            // 指定のビット位置の値を取得し、文字列に追加
            binaryString = ((value >> i) & 1) + binaryString;
        }

        // ビット数に満たない場合、左側に0を追加して補完
        while (binaryString.Length < bitCount)
        {
            binaryString = "0" + binaryString;
        }

        return binaryString;
    }
    
    public static void CopyIntArray(int[] source, int[] destination, int length)
    {
        // 指定された長さ分だけsourceからdestinationへコピーします
        for (int i = 0; i < length; i++)
        {
            destination[i] = source[i];
        }
    }

    public int BitStringToInt(string bitString)
    {
        int result = 0;
        for (int i = 0; i < bitString.Length; i++)
        {
            if (bitString[i] == '1')
            {
                result += (1 << (bitString.Length - i - 1));  // ビットシフトを使って各桁を加算
            }
        }
        return result;
    }

    public static float Std_FloatArray(float[] x) {
        float mean = Mean_FloatArray(x); // 平均値を計算
        float sumOfSquares = 0f;
        foreach (float value in x) {
            sumOfSquares += Mathf.Pow(value - mean, 2); // 各要素の偏差を2乗して加算
        }
        return Mathf.Sqrt(sumOfSquares / x.Length); // 偏差平方和の平均の平方根を返す
    }


    // 配列を文字列に変換するメソッド
    private string IntArrayToString(int[] array)
    {
        string result = "[";  // 開始のブラケット
        for (int i = 0; i < array.Length; i++)
        {
            result += array[i].ToString();  // 配列の値を文字列に変換して追加
            if (i < array.Length - 1)
            {
                result += ", ";  // 各値の間にカンマとスペースを追加
            }
        }
        result += "]";  // 終了のブラケット
        return result;
    }
            
}
