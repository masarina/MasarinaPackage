import torch
from torch import nn, optim

class PytorchDebuger:
    def __init__(self):
        pass

    @staticmethod
    def compare_model_optimizer_shapes(model, optimizer):
        """
        モデルの各レイヤーのshapeをOptimizerと比較し、不一致を表示する。
        """
        model_params = {name: param.shape for name, param in model.named_parameters()}
        optimizer_params = {f"optimizer_{i}": p.shape for i, p in enumerate(optimizer.param_groups[0]['params'])}

        mismatched_layers = []

        print("【Shape比較結果】")
        print("-" * 50)

        for name, model_shape in model_params.items():
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
    def compare_optimizer_optimizer_shapes(prev_optimizer, current_optimizer):
        """
        前回と今回のOptimizerのパラメータshapeを比較し、不一致を表示する。
        """
        prev_params = [p.shape for group in prev_optimizer.param_groups for p in group['params']]
        current_params = [p.shape for group in current_optimizer.param_groups for p in group['params']]

        print("【Optimizerパラメータ比較結果】")
        print("-" * 50)

        if prev_params == current_params:
            print("前回と今回のOptimizerのパラメータは一致しています！🎉")
        else:
            print("不一致のパラメータが存在します！")
            print(f"前回のパラメータ数: {len(prev_params)}, 今回のパラメータ数: {len(current_params)}")

            for i, (prev_shape, curr_shape) in enumerate(zip(prev_params, current_params)):
                if prev_shape != curr_shape:
                    print(f"不一致パラメータ: {i+1}, 前回: {prev_shape}, 今回: {curr_shape}")

    @staticmethod
    def compare_model_model_shapes(prev_model, current_model):
        """
        前回と今回のモデルのパラメータshapeを比較し、不一致を表示する。

        Args:
            prev_model (torch.nn.Module): 前回のモデル
            current_model (torch.nn.Module): 今回のモデル
        """
        prev_params = {name: param.shape for name, param in prev_model.named_parameters()}
        current_params = {name: param.shape for name, param in current_model.named_parameters()}

        mismatched_layers = []

        print("【モデルパラメータ比較結果】")
        print("-" * 50)

        for name, prev_shape in prev_params.items():
            curr_shape = current_params.get(name)
            if curr_shape is None:
                print(f"レイヤーが存在しません: {name}")
            elif prev_shape != curr_shape:
                mismatched_layers.append((name, prev_shape, curr_shape))
                print(f"不一致レイヤー: {name}, 前回: {prev_shape}, 今回: {curr_shape}")

        if not mismatched_layers:
            print("前回と今回のモデルのパラメータは一致しています！🎉")
        else:
            print("\n【不一致のレイヤー一覧】")
            for name, prev_shape, curr_shape in mismatched_layers:
                print(f"レイヤー名: {name}, 前回: {prev_shape}, 今回: {curr_shape}")
