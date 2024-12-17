import torch
from torch import nn, optim

class PytorchDebuger:
    def __init__(self):
        pass

    @staticmethod
    def compare_model_optimizer_shapes(model, optimizer):
        """
        モデルの各レイヤーの重み、バイアス、ベータ値のshapeをOptimizerのパラメータと比較し、
        不一致のレイヤーの名前とshapeを表示する関数。
        """
        # モデルのパラメータを取得
        model_params = {name: param.shape for name, param in model.named_parameters()}

        # Optimizerのパラメータを取得
        optimizer_params = {f"optimizer_{i}": p.shape for i, p in enumerate(optimizer.param_groups[0]['params'])}

        # 比較結果を格納するリスト
        mismatched_layers = []

        print("【Shape比較結果】")
        print("-" * 50)

        # モデルのパラメータとOptimizerのパラメータを比較
        for name, model_shape in model_params.items():
            # OptimizerパラメータとShapeを比較
            match_found = any(model_shape == opt_shape for opt_shape in optimizer_params.values())

            if not match_found:
                mismatched_layers.append((name, model_shape))
                print(f"不一致レイヤー: {name}, Shape: {model_shape}")

        if not mismatched_layers:
            print("全てのレイヤーのShapeがOptimizerに一致しています！🎉")
        else:
            print("\n【不一致のレイヤー一覧】")
            for layer_name, shape in mismatched_layers:
                print(f"レイヤー名: {layer_name}, Shape: {shape}")

    @staticmethod
    def compare_optimizers(prev_optimizer, current_optimizer):
        """
        前回のOptimizerと今回のOptimizerのパラメータを比較し、不一致がある場合に表示するメソッド。

        Args:
            prev_optimizer (torch.optim.Optimizer): 前回のOptimizer
            current_optimizer (torch.optim.Optimizer): 今回のOptimizer
        """
        # 前回と今回のOptimizerのパラメータを取得
        prev_params = [p.shape for group in prev_optimizer.param_groups for p in group['params']]
        current_params = [p.shape for group in current_optimizer.param_groups for p in group['params']]

        print("【Optimizerパラメータ比較結果】")
        print("-" * 50)

        # 比較
        if prev_params == current_params:
            print("前回と今回のOptimizerのパラメータは一致しています！🎉")
        else:
            print("不一致のパラメータが存在します！")
            print(f"前回のOptimizerのパラメータ数: {len(prev_params)}")
            print(f"今回のOptimizerのパラメータ数: {len(current_params)}")

            # Shapeの違いを確認
            for i, (prev_shape, curr_shape) in enumerate(zip(prev_params, current_params)):
                if prev_shape != curr_shape:
                    print(f"不一致パラメータ: {i+1}, 前回: {prev_shape}, 今回: {curr_shape}")
