   M S B A M L   `   `   `  ����?  ;Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DemoDemo  5   	       Demo.AdminWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Demo     5   	     � Title$7  1Управление пользователями��5   	   $	��600��5	   	   $	��900��6    � WindowStartupLocation$ CenterScreen=�5
   	   $��Segoe UI)�5   	   $��14(�6   .��5      � .+�5   
   �� -   5         ��Name$ Label1��5      $r��lДобавление пользователей. Изменение данных пользователей.��6    $
��Left=�5      $��30��6'   $��48,6,0,0q�63   $	��Top=�6E   �� 5   
   -   5        $ Label2��5      $2��,Имя нового пользователя��6    $
��Left=�6B   $��40��6]   $��	48,46,0,0q�6i   $	��Top=�5      �� 5   
   -   5        $ Label3��5      $!��Введите пароль��6    $
��Left=�69   $��30��6T   $��	48,90,0,0q�6`   $	��Top=�5      �� 5   
   -   5        $ Label4��5      $8��2Выберите роль пользователя��6    $
��Left=�6E   $��30��6`   $��
48,135,0,0q�6l   $	��Top=�5      �� 5   
   -   5        $ Label5��5      $��Фамилия��6    $
��Left=�62   $��30��6M   $��
48,180,0,0q�6Y   $	��Top=�5      �� 5   
   -   5        $ Label6��5      $��Имя��6    $
��Left=�6.   $��30��6I   $��300,180,0,0q�6U   $	��Top=�5      �� 5   
   -   5        $ Label7��5      $��Отчество��6    $
��Left=�63   $��30��6N   $��500,180,0,0q�6Z   $	��Top=�5      �� 5   
   -   5         ��Name$ TextBox1��5      $
��Left=�6$   $��33��6?   $	��250��6K   $��
248,46,0,0q�6W    �� TextWrapping$
 Wrap=�5      $	��Top=�6"   �� 5   
   -	   5        $ TextBox2��5      $
��Left=�6$   $��33��6?   $	��250��6K   $��
248,90,0,0q�6W   $
 Wrap=�5      $	��Top=�6"   �� 5    
   -
   5        $ TextBox3��5       $
��Left=�6$   $��33��6?   $	��150��6K   $��120,180,0,0q�6W   $
 Wrap=�5!      $	��Top=�6"    �� TextAlignment$ Center=�6:   �� 5"   
   -   5        $ TextBox4��5"      $
��Left=�6$   $��33��6?   $	��150��6K   $��340,180,0,0q�6W   $
 Wrap=�5#      $	��Top=�6"   $ Center=�6:   �� 5$   
   -   5        $ TextBox5��5$      $
��Left=�6$   $��33��6?   $	��150��6K   $��570,180,0,0q�6W   $
 Wrap=�5%      $	��Top=�6"   $ Center=�6:   �� 5'   
   -   5         ��Name$ typeRole��5'      $��14(�6%   $
��Left=�63   $��33��6N   $	��250��6Z   $	��Top=�5(      $��248,135,0,0q�6'   6>   �� 5*   
   -   5         ��Name$ button3��5*      $��Добавить��6"   $
��Left=�65   $��30��6P   $��90��6\   $��555,138,0,0q�5+      $	��Top=�6"    J� RenderTransformOrigin$ 0.189,0.601&�6:   �� 5,   
   -   5        $ button4��5,      $+��%Сохранить изменения��6"   $
��Left=�6@   $��30��6[   $	��130��6g   $��609,516,0,0q�5-      $	��Top=�6"   
����������	#FF4B9E446M   �� 5.   
   -   5        $ buttonClose��5.      $��Закрыть��6&   $
��Left=�68   $��30��6S   $��90��6_   $��768,516,0,0q�5/      $	��Top=�6"   
���  ��
�������6e   �� 50   
   -   5        $ buttonDelete��50      $��Удалить��6'   $
��Left=�69   $��30��6T   $��90��6`   $��674,138,0,0q�51      $	��Top=�6"   $ -0.133,0.735&�6U   �� 53   
   	 �� Orientation$	 Vertical=�6   $��Center=�6,   $	��Top=�6I   $��	5,230,5,5q�6a   .+�54      &   System.Windows.Controls.DataGrid  -   5        $��GridUser��54      
   AutoGenerateColumns$
 False��6'   $	��190��6C   $	��750��6P   $
��True��55         Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 65   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *ID+  58      $ Фамилия��6)   $ * 6:    6D   �� *	surname+  59      $ Имя��6)   $ * 66    6@   �� *name+  5:      $ Отчество��6)   $ * 6;    6E   �� *	othcestvo+  5;      $ Роль��6)   $ * 67    6A   �� *role2+  5<      $ 
Логин��6)   $ * 68    6B   �� *login+  5=      $ Пароль��6)   $ * 69    6C   �� *
password+  5>      $ Кол-во вводов��6)   $ * 6@    6J   �� *count+  5?      $ Активность��6)   $ * 6=    6G   �� *active+  5@      $ Дата входа��6)   $ * 6=    6G   �� *date+5B      5C      5D      5E      