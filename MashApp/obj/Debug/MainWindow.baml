   M S B A M L   `   `   `  ����B  >MashApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MashAppMashApp  5   	       MashApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:MashApp     5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	     � Title$  MashApp Client��5	   	   $��450.4��6    $	��800��6/   ���	#FF202020$	��800��6R   $	��450��6a    � Icon$ MashAppIcon.ico��6q    � WindowStartupLocation$ CenterScreen=�6�   .��5
      � -   5         �Name$ mainGrid��5
      $��0q�6   ���#202020.+�5   
   � -   5        $ firstTimeGrid��5      $y�	Collapsed=�6&   .+�5      �� -   5         ��Name$ 	introText��5      $��Hello!��6'   $��0,184,0,180q�68    J� RenderTransformOrigin$ 0.5,0.5&�6M   $��Dosis)�6m   $��22(�6�   $��Center=�6�   $��Center=�6�   $��0q�6�    � UseLayoutRounding .  ���	#FFE6E6E6 J� Opacity$ 0Q�6  �� 5      -   5         ��Name$ 	placeName��5      $��Center=�6)   $��Center=�6M   $��Center=�6o   $��40��6�   $��0,243,0,137.8q�6�   	 �� TextWrapping$
	 Wrap=�6�   $��My Place��6�   $��Center=�6�   $	��256��6�   $��Dosis)�6�   $y�	Collapsed=�6  
���   ����	#FF202020���	#FFFBC02D$��18(�6g  $ 0Q�6u  �� 5      -   5        
 ��Name$
 nameEntered��5      $
��Done��6*   $��Center=�69   $��	0,200,0,0q�6V   $��Center=�6i   $��75��6�   $��Dosis)�6�   $��26��6�   
���   ����	#FF202020���	#FFFBC02D$��18(�6�   $ 0Q�6�   $y�	Collapsed=�6
  �� 5      -   5         ��Name$ selectYoutube��5       �� BitmapScalingMode$ HighQuality=�6+   $��64��6Y   $��0,150,104,0q�6e   $��Center=�6z   $��Center=�6�   $��64��6�   $��youtube.png��6�   $ 0.5,0.5&�6�   $y�	Collapsed=�6�   $ 0Q�6	  �� 5      -   5        $ selectFolder��5      $ HighQuality=�6*   $��64��6X   $��0,150,-100,0q�6d   $��Center=�6z   $��Center=�6�   $��64��6�   $��
folder.png��6�   $ 0.5,0.5&�6�   $y�	Collapsed=�6�   $ 0Q�6  5      � 5   
   -   5        $ startupGrid��5      $y�	Collapsed=�6$   .+�5      �� -	   5        $ connectionLabel��5      $��
Connecting��6-   $��
0,-122,0,0q�6B   $ 0.5,0.5&�6V   $��50��6v   $��Dosis)�6�   $��20(�6�   $	��300��6�   $��Center=�6�   $��Center=�6�   $��0q�6�    .  ���	#FFE6E6E66  "  FontAwesome.WPF.ImageAwesome  5      -
   5          Name$ 
loadingBar��5         Icon$ Refresh=�62      Spin . $��64��6M   $��64��6Y      
Foreground �	#FFE6E6E66d   �� 5      -   5        $ 
logoHolder��5      $ HighQuality=�6(   $	��128��6V   $��0q�6c   $	��128��6n   $ 0.5,0.5&�6z   $��logo.png��6�   $ 0Q�6�   5      � 5   
   -   5        $ dataGrid��5      $y�	Collapsed=�6!   $	��4,0q�68    � RowDefinitions 5      �� 5      $��64 �6    �� 5       � ColumnDefinitions 5      �� 5      $
��404* �6#   �� 5      $
��389* �6#   .+�5       �� -   5        $ TopLeftLogo��5       $��10,5,0,5q�6A   $��logo.png��6S   $
��Left=�6e   $��54��6�   $ HighQuality=�6�   �� 5!      -   5         ��Name$ inQueue��5!      $��4,0,4,50q�6'   $��1��69   ���	#FF313131
�������$��Dosis)�6p   ��6�   Z� �� 5"      -   5        $ allSongs��5"      $��4,0,4,50q�6(   $��1��6:   $��1��6J   
����������	#FF313131$��Dosis)�6�   ��6�   Z� r� 5#      -   5         r�Name$ mediaPlayer��5#      $
��Left=�60   $��26��6K   $��12,336.4,0,0q�6W   $��1��6m   $	��Top=�6z   $	��382��6�   $y�Hidden=�6�   (  "System.Windows.Controls.WebBrowser  5$      -   5          Name$ VideoPlayer��5$   0   $y�	Collapsed=�6   $��4,0,4,50q�6E   $��1��6W   �� 5%      $��Right=�6   $��0,0,4,4q�64   $
��Wrap=�6E   $��Bottom=�6Y   $��Dosis)�6t   $��20(�6�   
�������$��Queue��6�   $ 
0.491,0.53&�6�   $��26��6�   �� 5&      $��0,0,4,4q�6   $
��Wrap=�6)   $��Bottom=�6=   $��Dosis)�6X   $��20(�6k   
�������$��Available Songs��6�   $ 
0.491,0.53&�6�   $��1��6�   $��Right=�6�   $	��120��6�   $��26��6�   �� 5'      -   5        $
 
changeName��5'      $��Change Name��6)   $��1��6?   $��0,0,4,10q�6O   $��1��6a   $��80��6n   $��32��6y   $��Dosis)�6�   $��Bottom=�6�   $��Right=�6�   ���	#FF202020���	#FFFBC02D��6�   Z� �� 5(      -   5         ��Name$ currentlyPlaying��5(      $
��Left=�62   $��4,0,0,10q�6M   $��1��6_   $
��Wrap=�6l   $��Currently Playing��6�   $��Bottom=�6�   $��32��6�   $	��390��6�   $��Dosis)�6�   
�������6�   �� 5)      -   5        $
 
changeList��5)      $��Change List��6)   $��1��6?   $��	0,0,94,10q�6O   $��1��6b   $��80��6o   $��32��6z   $��Dosis)�6�   $��Bottom=�6�   $��Right=�6�   ���	#FF202020���	#FFFBC02D��6�   Z� �� 5*      -   5        $
 	showVideo��5*      $y�	Collapsed=�6(   $��
Show Video��6?   $��1��6T   $��4,0,0,10q�6d   $��1��6v   $��32��6�   $��Dosis)�6�   $��Bottom=�6�   ���	#FF202020���	#FFFBC02D$
��Left=�6  $��80��6  ��6�   Z� 6(  5+      5,      5-      