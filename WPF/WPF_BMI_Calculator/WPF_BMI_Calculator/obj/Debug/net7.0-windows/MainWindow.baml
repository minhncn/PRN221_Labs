   M S B A M L   `   `   `  ����M  IWPF_BMI_Calculator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WPF_BMI_CalculatorWPF_BMI_Calculator  5   	   #    WPF_BMI_Calculator.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:WPF_BMI_Calculator     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� $��30(�6   $��BMI Calculator��6   $��Center=�67   $��0,83,0,0q�6T   $	��Top=�6f   �� 5   
   $��20(�6   $��Weight��6   $
��Left=�6/   $��	138,0,0,0q�6J   $��Center=�6]   �� 5   
   $��20(�6   $��Height��6   $
��Left=�6/   $��138,259,0,0q�6J   $	��Top=�6_   �� 5   
   -   5        $��	txtWeight��5      $
��Left=�6#   $��18(�6>   $��	222,0,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $��Center=�6s   $	��152��6�   $��29��6�   �� 5   
   -   5        $��	txtHeight��5      $
��Left=�6#   $��18(�6>   $��222,267,0,0q�6L   $
 Wrap=�6a   $	��Top=�6u   $	��152��6�   $��29��6�   �� 5   
   -   5        $��Submit��5      $
��Left=�6"   $��298,333,0,0q�6=   $	��Top=�6R    J� RenderTransformOrigin$ -6.275,7.234&�6   $��37��6�   $��76��6�   �� 5   
   -   5        $��ResultLabel��5   )   $��20(�6   $�� ��6   $
��Left=�6>   $��558,217,0,0q�6Y   $	��Top=�6n   $��66��6�   �� 5   
   -   5        $��ResultStatus��5   ;   $��20(�6   $
��Bold"�6   $�� ��60   $
��Left=�6P   $��512,263,0,0q�6k   $	��Top=�6�   $	��158��6�   �� 5   
   $	��BMI��6   $��20(�6   $
��Left=�6,   $��430,218,0,0q�6G   $	��Top=�6\   �� 5   
   $��Status��6   $��20(�6!   $
��Left=�6/   $��430,260,0,0q�6J   $	��Top=�6_   �� 5   
   -   5        $��Clear��5      $
��Left=�6!   $��222,333,0,0q�6<   $	��Top=�6Q   $��37��6i   $��66��6u   6�   5      5      