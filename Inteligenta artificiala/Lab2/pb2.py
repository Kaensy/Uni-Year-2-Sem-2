import matplotlib.pyplot as plt
import matplotlib.image as mpimg
import os
from skimage.color import rgb2gray, rgba2rgb
from PIL import Image
from skimage.filters import gaussian
from skimage.feature import canny

def visualize_image():
    image_files = os.listdir('images')

    img = mpimg.imread(os.path.join('images', image_files[0]))

    plt.imshow(img)
    plt.show()


def tabelar():
    image_files = os.listdir('images')

    # Define the size of the grid
    grid_size = min(int(len(image_files) ** 0.5), 3)

    # Create a new figure
    plt.figure(figsize=(10, 10))

    # Loop over the image files and add each to the grid
    for i, image_file in enumerate(image_files[:grid_size * grid_size], 1):
        # Open the image file
        img = Image.open(os.path.join('images', image_file))

        # Resize the image
        img = img.resize((128, 128))

        # Add the image to the grid
        plt.subplot(grid_size, grid_size, i)
        plt.imshow(img)
        plt.axis('off')

    # Show the plot with all images
    plt.show()

def visualize_gray_images():
    image_files = os.listdir('images')

    # Define the size of the grid
    grid_size = min(int(len(image_files) ** 0.5), 3)

    # Create a new figure
    plt.figure(figsize=(10, 10))

    # Loop over the image files and add each to the grid
    for i, image_file in enumerate(image_files[:grid_size * grid_size], 1):
        # Read the image file
        img = mpimg.imread(os.path.join('images', image_file))

        # Convert the image to RGB if it's RGBA
        if img.ndim == 3 and img.shape[2] == 4:
            img = rgba2rgb(img)

        # If the image is not grayscale, convert it to grayscale
        if img.ndim == 3:
            gray_img = rgb2gray(img)
        else:
            gray_img = img

        # Add the image to the grid
        plt.subplot(grid_size, grid_size, i)
        plt.imshow(gray_img, cmap='gray')
        plt.axis('off')

    # Show the plot with all images
    plt.show()


def blur(image_file):
    img = mpimg.imread(os.path.join('images', image_file))

    # Blur the image
    blurred_img = gaussian(img, sigma=10, channel_axis=-1)  # Increase sigma value

    # Create a new figure
    plt.figure(figsize=(10, 5))

    # Display the original image
    plt.subplot(1, 2, 1)
    plt.imshow(img)
    plt.title('Original')
    plt.axis('off')

    # Display the blurred image
    plt.subplot(1, 2, 2)
    plt.imshow(blurred_img)
    plt.title('Blurred')
    plt.axis('off')

    # Show the plot with both images
    plt.show()


def edges(image_file):
    # Read the image file
    img = mpimg.imread(os.path.join('images', image_file))

    # Convert the image to grayscale if it's not
    if img.ndim == 3:
        img = rgb2gray(img)

    # Perform edge detection
    edges = canny(img, sigma=3)

    # Create a new figure
    plt.figure(figsize=(10, 5))

    # Display the original image
    plt.subplot(1, 2, 1)
    plt.imshow(img, cmap='gray')
    plt.title('Original')
    plt.axis('off')

    # Display the edge-detected image
    plt.subplot(1, 2, 2)
    plt.imshow(edges, cmap='gray')
    plt.title('Edge Detection')
    plt.axis('off')

    # Show the plot with both images
    plt.show()

visualize_image()
tabelar()
visualize_gray_images()
blur('LeCun.jpg')
edges('Karpaty.jpg')