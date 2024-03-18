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

    grid_size = min(int(len(image_files) ** 0.5), 3)

    plt.figure(figsize=(10, 10))

    for i, image_file in enumerate(image_files[:grid_size * grid_size], 1):

        img = Image.open(os.path.join('images', image_file))

        img = img.resize((128, 128))

        plt.subplot(grid_size, grid_size, i)
        plt.imshow(img)
        plt.axis('off')


    plt.show()

def visualize_gray_images():
    image_files = os.listdir('images')

    grid_size = min(int(len(image_files) ** 0.5), 3)

    plt.figure(figsize=(10, 10))

    for i, image_file in enumerate(image_files[:grid_size * grid_size], 1):

        img = mpimg.imread(os.path.join('images', image_file))

        if img.ndim == 3 and img.shape[2] == 4:
            img = rgba2rgb(img)

        if img.ndim == 3:
            gray_img = rgb2gray(img)
        else:
            gray_img = img

        plt.subplot(grid_size, grid_size, i)
        plt.imshow(gray_img, cmap='gray')
        plt.axis('off')

    plt.show()


def blur(image_file):
    img = mpimg.imread(os.path.join('images', image_file))

    blurred_img = gaussian(img, sigma=10, channel_axis=-1)

    plt.figure(figsize=(10, 5))

    plt.subplot(1, 2, 1)
    plt.imshow(img)
    plt.title('Original')
    plt.axis('off')

    plt.subplot(1, 2, 2)
    plt.imshow(blurred_img)
    plt.title('Blurred')
    plt.axis('off')

    plt.show()


def edges(image_file):

    img = mpimg.imread(os.path.join('images', image_file))

    if img.ndim == 3:
        img = rgb2gray(img)

    edges = canny(img, sigma=3)

    plt.figure(figsize=(10, 5))

    plt.subplot(1, 2, 1)
    plt.imshow(img, cmap='gray')
    plt.title('Original')
    plt.axis('off')

    plt.subplot(1, 2, 2)
    plt.imshow(edges, cmap='gray')
    plt.title('Edge Detection')
    plt.axis('off')

    plt.show()

visualize_image()
tabelar()
visualize_gray_images()
blur('LeCun.jpg')
edges('Karpaty.jpg')