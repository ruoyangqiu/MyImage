# Image Processor

## Introduction
This is a Image processor API. User can use this to manipulate one image.

### Functionality
The processor has following functionalities:

* Flip horizontal and vertical
* Convert to grayscale
* Resize
* Generate a thumbnail
* Rotate left
* Rotate right
* Rotate +/- n degrees

### Developing tool

This project is develop with ASP.Net Core. 
All the Image Manipulation are based on System.Drawing.Common package.

### Guide

Using Http Call 
*	Upload Image through a POST Http call
    *	user needs to provide a valid image URL, it will download the image from URL
    *	Code: 
        *	  curl -X POST "https://localhost:44375/Image?url=image.jpg" -H "accept: */*"
    *	Response:
        *	200 for Success
        * 400 if user does not input a URL, it will response an InvalidUrlError
        *	400 if user does not input a valid image URL or the image cannot be downloaded from URL, it will respond an InvalidUrlError

*	Get the current Image(GET)
    *	It will display the Current Image, after editing
    *	Code:
        *		curl -X GET "https://localhost:44375/Image" -H "accept: */*"
    *	Response:
        *	200 for Success and display the image
        *	400 if user hasn’t uploaded an image yet. It will also response a NoImageError

*	####Flip Image(PUT)
    *	It will flip the image either vertical or horizontal. User needs to input horizontal or vertical to provide an orientation to flip.
    *	Code:
        *	  curl -X PUT "https://localhost:44375/Image/flipping?orientation=Horizontal" -H "accept: */*"
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also respond a NoImageError
        *	400 if User didn’t provide an orientation. And respond an EmptyOrientationError
        *	400 if user inputs the words other than vertical and horizontal, ignore case. It will also respond an InvalidOrientationError

*	Grayscale (PUT)
    *	Transform the image to grayscale
    *	Code:
        *	  curl -X PUT "https://localhost:44375/Image/grayscale" -H "accept: */*"
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also respond a NoImageError

*	Resize(PUT)
    *	Resize the image to the fit the new width and height given by user. For example, resizing to 400*150
    *	Code:
        *		curl -X PUT "https://localhost:44375/Image/resizing?width=400&height=150" -H "accept: */*"
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also respond a NoImageError
        *	400 if any input width is negative or zero. It will also respond an InvalidWidthError
        *	400 if any input Height is negative or zero. It will also respond an InvalidHeightError

*	####Left Rotation(PUT)
    *	Rotation the image 90 degree counterclockwise
    *	Code:
        *	  curl -X PUT "https://localhost:44375/Image/leftrotation" -H "accept: */*"
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also response a NoImageError

*	Right Rotation(PUT)
    *	Rotation the image 90 degree clockwise
    *	Code:
        * 		curl -X PUT "https://localhost:44375/Image/rightrotation" -H "accept: */*"
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also respond a NoImageError

*	Rotate an image by n degree (PUT): 
    *	Rotate the image clockwise based on the angle given by the user. If User didn’t input any degree it will rotate 0 degree. For example, if a user wants to rotate 23 degree
    *	Code:
        *	  curl -X PUT “https://localhost:44375/Image/anglerotation?angle=23” -H “accept: */*”
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also respond a NoImageError

*	Generate a Thumbnail(PUT)
    *	Make a thumbnail of current image
    *	Code:
        *	  curl -X PUT "https://localhost:44375/Image/thumbnail" -H "accept: */*"
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also respond a NoImageError

*	Get the Thumbnail(GET)
    *	Get the thumbnail and display it
    *	Code:
        *		curl -X GET "https://localhost:44375/Image/thumbnail" -H "accept: */*"
    *	Response:
        *	200 for Success 
        *	400 if user hasn’t uploaded an image yet. It will also respond a NoImageError




