   M S B A M L   `   `   `  ����N  JGestionnaireBaseBTS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:GestionnaireBaseBTSGestionnaireBaseBTS  5   	   #    GestionnaireBaseBTS.Connexion   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:GestionnaireBaseBTS     5   	     � Title$  	Connexion��5   	   $	��280��6   $	��320��6(    � 
ResizeMode$ NoResize=�5	   	    � Icon$ Images/Nodes.png��6   ��5
      �� 5   
    �� ImageSource$ Images/DataCenter.jpg��6   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      $��* �6   .+�5   
   �� $��Images/data-Network.png��6   $��70��61   $
��Left=�6<   �� 5   
   $��Gestionnaire De Bases��6   $��Center=�61   $��Right=�6L   $��22(�6h   
�������$��0,0,5,0q�6�   �� 5   
    �� CornerRadius$ 6��6   
�������$��2q�66    �� Padding$ 8q�6J   $��1��6V   ._�5      � $��1��6    5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$    � ColumnDefinitions 5      �� 5       $
��Auto �6'   �� 5!      $
��Auto �6'   .+�5$      �� $��Login��6   
�������$��	25,10,0,0q�6<   $��12(�6O   �� 5%      $��Images/User-Profile.png��6   $��15��69   $
��Left=�6D   $��5,0,0,0q�6_   �� 5&      -   5         ��Name$ tbLogin��5&      $��1��6+   $	��250��68   $��5q�6D   $��2��6O   �� 5(      $��2��6   $��Password��6)   
�������$��25,0,0,0q�6L   $��12(�6^   �� 5)      $��Images/Login-01.png��6   $��2��65   $��15��6B   $
��Left=�6M   $��5,0,0,0q�6h   G� 5*      -   5        	 G�Name$	 
tbPassword��5*      $��3��62   $��5q�6?   $��2��6J   �� 5,      -   5        
 ��Name$
 btnConnexion��5,      $��4��6/   $��1��6<   $��Right=�6L   $��5,10,8,5q�6h   $	��120��6�   $��20��6�   
�������.��5-      ��  �� Orientation$ 
Horizontal=�6!   .+�5.      �� $��	Connexion��6$   ���	#FF0F6A99$
��Bold"�6L   �� 5/      $#��Images/Arrowhead-Right-01.png��6    $��10��6G   $��10,0,0,0q�6R   50      51      52      53      54      55      