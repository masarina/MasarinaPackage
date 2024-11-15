
/// this file made by Masarina(Twitter_@Masarina002)
/// meamメソッドはMean_FloatArrayに名前を変更しました(2024-11-15)

using UdonSharp;
using UnityEngine;

public class RinaNumpy : UdonSharpBehaviour
{
    //
    public static float Sum_FloatArray(float[] x) {
        float sum = 0;
        for (int i = 0; i < x.Length; i++) {
            sum += x[i];
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
        float sum = 0;
        for (int i = 0; i < x.Length; i++) 
        {
            sum += x[i];
        }
        return sum / x.Length;
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
        for (int i = 0; i < x.Length; i++) 
        {
            sumOfSquares += Mathf.Pow(x[i] - mean, 2);
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
        float mean = Mean_FloatArray(x);
        float sumOfSquares = 0f;
        for (int i = 0; i < x.Length; i++) {
            sumOfSquares += Mathf.Pow(x[i] - mean, 2);
        }
        return Mathf.Sqrt(sumOfSquares / x.Length);
    }



    // 配列を文字列に変換するメソッド
    private string IntArrayToString(int[] array)
    {
        // 配列を文字列に変換
        int[] tempArray = new int[array.Length]; // コピー用の一時配列
        this.CopyIntArray(array, tempArray, array.Length); // 配列をコピー
        string result = "";

        for (int i = 0; i < tempArray.Length; i++)
        {
            result += tempArray[i].ToString(); // 各要素を文字列化して結合
            if (i < tempArray.Length - 1)
            {
                result += ", "; // 区切り文字を追加
            }
        }

        return result;
    }
            
}
