using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


public class RinaNumpy : UdonSharpBehaviour
{
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
    
    //
    public static float[] Multiply_FloatArray_FloatArray(float[] x, float[] y) {
        if (x.Length != y.Length) throw new System.ArgumentException("Arrays must be of equal length.");
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
        if (x.Length != y.Length) throw new System.ArgumentException("Arrays must be of equal length.");
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
    public static float DotProduct_FloatArray_FloatArray(float[] x, float[] y)
    {
        if (x.Length != y.Length) throw new System.ArgumentException("Arrays must be of equal length for dot product.");
        
        float result = 0;
        for (int i = 0; i < x.Length; i++)
        {
            result += x[i] * y[i];
        }
        return result;
    }

    //
    public static float[] DotProduct_FloatArray2D_FloatArray(float[][] A, float[] x)
    {
        if (A[0].Length != x.Length) throw new System.ArgumentException("Matrix columns and vector size must match for dot product.");

        float[] result = new float[A.Length];
        for (int i = 0; i < A.Length; i++)
        {
            for (int j = 0; j < x.Length; j++)
            {
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
    
    //
    public static float Mean(float[] x)
    {
        float sum = 0f;
        foreach (float value in x)
        {
            sum += value; // 配列の各要素を合計
        }
        return sum / x.Length; // 合計を要素数で割って平均を求める
    }
    
    
    public static float[] Power_FloatArray_Float(float[] x, float y) {
        float[] result = new float[x.Length];
        for (int i = 0; i < x.Length; i++) {
            result[i] = Mathf.Pow(x[i], y);
        }
        return result;
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
    
    public static float[] Sum_FloatArray2d_Float_axis0(float[][] x)
    {
        if (x.Length == 0 || x[0].Length == 0) return new float[0]; // 空の配列の場合は空の配列を返す
    
        float[] sum = new float[x[0].Length]; // 列の数だけ要素を持つ配列を初期化
        for (int i = 0; i < x.Length; i++) // 行のループ
        {
            for (int j = 0; j < x[i].Length; j++) // 列のループ
            {
                sum[j] += x[i][j]; // 各列の要素を足し合わせる
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
    
    public static float Std_FloatArray(float[] x) 
    {
        float mean = Mean_FloatArray(x); // 平均値の計算
        float sumOfSquares = 0f;
        for (int i = 0; i < x.Length; i++) {
            sumOfSquares += Mathf.Pow(x[i] - mean, 2); // 各要素から平均を引いて、二乗
        }
        return Mathf.Sqrt(sumOfSquares / x.Length); // その平均の平方根を取る
    }


    // 配列に新しい要素を追加するメソッド
    public int[] AppendElement_Int(int[] originalArray, int newElement)
    {
        // 新しい配列を作成
        int[] newArray = new int[originalArray.Length + 1];

        // 元の配列の要素を新しい配列にコピー
        for (int i = 0; i < originalArray.Length; i++)
        {
            newArray[i] = originalArray[i];
        }

        // 新しい要素を追加
        newArray[originalArray.Length] = newElement;

        return newArray;
    }

    // 二次元配列の各行の要素数をカウントして、1次元配列として返すメソッド【数える、1行あたり】
    public int[] CountElementsInRows_StrAarray2d(string[][] jaggedArray)
    {
        // 各行の要素数を格納するための1次元配列を作成
        int[] elementCounts = new int[jaggedArray.Length];

        // 各行の要素数をカウント
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            elementCounts[i] = jaggedArray[i].Length;
        }

        return elementCounts; // 1次元配列を返す
    }

    // 二次元配列を複製するメソッド
    public string[][] Clone_Array2d(string[][] original)
    {
        // 元の配列と同じ長さの新しい配列を作成
        string[][] clone = new string[original.Length][];

        // 各行をコピー
        for (int i = 0; i < original.Length; i++)
        {
            // 各行の長さを取得して新しい配列を作成
            clone[i] = new string[original[i].Length];
            for (int j = 0; j < original[i].Length; j++)
            {
                clone[i][j] = original[i][j]; // 要素をコピー
            }
        }

        return clone; // 複製した配列を返す
    }


    // 入力のジャグ配列と同じ行列数のint[][]を作成し、要素を0で初期化するメソッド
    public int[][] CreateZeroInitialized_Array2d(string[][] original)
    {
        // 元の配列と同じ長さの新しい配列を作成
        int[][] result = new int[original.Length][];

        // 各行の長さに合わせて新しい配列を初期化し、要素を0で埋める
        for (int i = 0; i < original.Length; i++)
        {
            result[i] = new int[original[i].Length];
            for (int j = 0; j < result[i].Length; j++)
            {
                result[i][j] = 0; // 要素を0で初期化
            }
        }

        return result; // 初期化された配列を返す
    }


    // 入力のジャグ配列と同じ行列数のstring[][]を要素"None"で初期化して返すメソッド
    public string[][] InitializeJagged_StrArray(string[][] inputArray)
    {
        // 入力配列と同じ行数の新しい配列を作成
        string[][] initializedArray = new string[inputArray.Length][];

        // 各行を"None"で初期化
        for (int i = 0; i < inputArray.Length; i++)
        {
            // 各行の長さを取得して新しい配列を作成
            initializedArray[i] = new string[inputArray[i].Length];
            for (int j = 0; j < inputArray[i].Length; j++)
            {
                initializedArray[i][j] = "None"; // 要素を"None"で初期化
            }
        }

        return initializedArray; // 初期化した配列を返す
    }


    // 二次元配列をディープコピーするメソッド
    public string[][] Copy_Array2d(string[][] inputArray)
    {
        // 入力配列と同じ行数の新しい配列を作成
        string[][] outputArray = new string[inputArray.Length][];

        // 各行をコピー
        for (int i = 0; i < inputArray.Length; i++)
        {
            outputArray[i] = new string[inputArray[i].Length];
            for (int j = 0; j < inputArray[i].Length; j++)
            {
                outputArray[i][j] = inputArray[i][j]; // 要素をコピー
            }
        }

        return outputArray; // コピーした配列を返す
    }

}