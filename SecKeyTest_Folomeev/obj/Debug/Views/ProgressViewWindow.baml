   M S B A M L   `   `   `  ����N  JSecKeyTest_Folomeev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:SecKeyTest_Folomeev.ViewModelsSecKeyTest_Folomeev.ViewModels  5   	   2    ,SecKeyTest_Folomeev.Views.ProgressViewWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;VM,clr-namespace:SecKeyTest_Folomeev.ViewModels     5   	     � Title$  Прогресс...��5   	   $	��100��6   $	��250��6*    � WindowStartupLocation$ CenterScreen=�66    � DataContext 5	      6  0SecKeyTest_Folomeev.ViewModels.ProgressViewModel  5
   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��107* �6   �� 5      $
��135* �6   .+�5   
   � -   5         �Name$ prgBar��5      $
��Left=�6&   $��20��6A   $��	75,15,0,0q�6M   $	��Top=�6`   $	��157��6x    � Minimum$ 0Q�6�    � Maximum$	 100Q�6�   $��2��6�   �� 5   
   -   5         ��Name$ label��5      $��Прогресс:��6   $
��Left=�63   $��	10,10,0,0q�6N   $	��Top=�6a   �� 5   
   -   5        $ label1��5      $��0��6    $
��Left=�6,   $��	37,12,0,0q�6G   $	��Top=�6Z    J� RenderTransformOrigin$ 2.438,0.846&�6r   $��1��6�   5      5      