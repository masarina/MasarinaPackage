


class PytorchDebuger:
    def __init__(self):
        pass
        
    def compare_model_optimizer_shapes(model, optimizer):
        """
        モデルの各レイヤーの重み、バイアス、ベータ値のshapeをOptimizerのパラメータと比較し、
        不一致のレイヤーの名前とshapeを表示する関数。
    
        Args:
            model (torch.nn.Module): PyTorchモデル
            optimizer (torch.optim.Optimizer): Optimizer
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
    