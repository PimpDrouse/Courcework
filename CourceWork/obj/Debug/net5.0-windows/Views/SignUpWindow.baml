   M S B A M L   `   `   `  ����E  ACourceWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:CourceWork.ViewModelsCourceWork.ViewModels  5   	   #    CourceWork.Views.SignUpWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:CourceWork.ViewModels     5   	   -   5          �Name$  	SingUpWin��5   	    � Title$ SignUp��5	   	   $	��800��5
   	   $	��800��5   	    � DataContext 5      +   %CourceWork.ViewModels.SignUpViewModel  5   
   .��5      �  � ShowGridLines$
 True��6    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��30(�6   $��Italic$�6"   $��Center=�65   $��Center=�6R   $��Sing up��6m   $��0��6|   �� 5   
   $��Center=�6   $��1��60   $��2��6=   .+�5      �� $��Center=�6   $��Login��65   �� 5       �� TextAlignment$ Center=�6-   $��0,0,0,20q�6D   ��6   �� *Login+6D   �� 5       $��Center=�6   $��Password��65   �� 5!      $ Center=�60   $��0,0,0,20q�6G   ��6   �� *
Password+6G   �� 5#      $��Center=�6   $
��Role��65   �� 5$      -   5         ��Name$ RoleBox��5$       �� Text$ -- Select Role --��6l    �� 
IsEditable$
 True��6�   	 �� 
IsReadOnly$
	 True��6�   $��Center=�6�   ��6(   �� *Roles+
 �� SelectedItem
 6F   �� *SelectedRole+6�   5&      �� 5(   
   $��Center=�6   $��SingUp��6.   $��3��6�   $��Center=�6�   ��6?   �� *SignUpCommand+��6a   ��  �� ElementName$ 	SingUpWin��6�   5)      5*      