   M S B A M L   `   `   `  ����?  ;Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DemoDemo  5   	       Demo.UserWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Demo     5   	     � Title$%  Добро пожаловать��5   	   $	��450��5	   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�5
   	   ���	#FFF4F6F8$��Segoe UI)�5   	   $��16(�5   	   .��5      � $��30q�6   .+�5   
   ��  �� CornerRadius$ 15��6   
������� �� Padding$ 30q�66      DropShadowEffect J� Effect# �   5      ._�5      �� $��Center=�6   $��Center=�66   .+�5      �� $7��1Добро пожаловать в систему��6   $��28(�5      $
��Bold"�6*   ���#2C3E50 �� TextAlignment$ Center=�5      $��0,0,0,20q�5      �� 5      -   5         ��Name$ 	LabelUser��5      $G��AВы авторизовались как пользователь��5      $��18(�5      ���#34495E$ Center=�5      $��0,0,0,40q�5      �� 5!      -   5         ��Name$ btnExit��5!      $��
Выйти��6*   $	��150��5"      $��45��6%   ���#E74C3C
�������$��SemiBold"�5$       � Cursor$
 Hand��5%      $��0q�5&      $��10q�5'      $��Center=�5(      ��5+      �� 5,      	 �� 
TargetType"	 ��6    .�5-      �� 
 �� Property
 � �� �� Value 5.   "   �� 5/   &    �� 
TargetType" ��66   .R�50   *   �� #��z��61   $ 10��51   1   ._�52   .   �� $��Center=�6?   $��Center=�6\   53   +   54   '   56       �� Triggers 57      P� 58   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �59   &   �� 
 � �� �#C0392B6C   5:   #   P� 5;   "    � ��$
 True��6?   . �5<   &   �� 
 � �� �#A932266C   5=   #   5?      5A      5C      5D      5E       � 	Resources 5G      3  -System.Windows.Media.Effects.DropShadowEffect   ShadowDepth   
BlurRadius   Opacity��%B   &	          5H   
   $ 4Q�64   $ 10Q�6D   $	 0.3Q�6T   5I      5K      