   M S B A M L   `   `   `  ����?  ;Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DemoDemo  5   	       Demo.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Demo     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � $��40q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Логин:��6   $��0��6!   $��Center=�6.   $��0,0,0,5q�6K   �� 5   
   -   5         ��Name$ txtLogin��5      $��1��6$   $	��250��61   $��Center=�6=   $��0,0,0,15q�6Z   �� 5   
   $��Пароль:��6   $��2��6"   $��Center=�6/   $��0,0,0,5q�6L   G� 5   
   -   5         G�Name$ txtPassword��5      $��3��6+   $	��250��68   $��Center=�6D   $��0,0,0,20q�6a   �� 5   
   -   5         ��Name$ btnLogin��5      $��
Войти��6#   $��4��63   $	��120��6@   $��Center=�6L   6i   5      5      