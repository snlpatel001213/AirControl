.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            .. rubric:: Aircontrol Camera
               API\ `¶ <#Aircontrol-Camera-API>`__
               :name: Aircontrol-Camera-API

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            .. rubric:: Camera Placement\ `¶ <#Camera-Placement>`__
               :name: Camera-Placement

            AirControl Airplane has two cameras. One is in the Cockpit
            and another is a follow camera. Both the camera can be used
            for capturing screenshots while in flight.

            .. rubric:: Python API\ `¶ <#Python-API>`__
               :name: Python-API

            Python API has a ``set_camera`` function to set the camera
            properties. ``set_camera`` function takes the following
            arguments:

            -  InputControlType (str, optional): It can be either
               ``Code`` or ``Other``. This is to control the internal
               mechanism and prevent repeated calling in already set
               variables.
            -  If ``InputControlType`` is set to 'Code', the camera
               cannot be controlled from Keyboard or Joystick. If
               ``InputControlType`` is set to 'Other', the camera can be
               only controlled from Keyboard or Joystick. Defaults to
               "Code".
            -  ActiveCamera (int, optional): Aircontrol Airplane has two
               cameras inside the Cockpit and outside the Airplane. The
               Camera inside the Cockpit is indexed as 0. The outside of
               the Airplane is indexed as 1. ``ActiveCamera`` can be
               used to select the scene camera. Defaults to 1.
            -  IsCapture (bool, optional): ``Iscapture`` if true the
               screenshot will be captured. Defaults to False.
            -  CaptureCamera (int, optional): ``CaptureCamera`` defines
               which camera should be used for capturing the scene.
               Defaults to 1.
            -  CaptureType (int, optional): Choose between different
               capture types. Defaults to 1.
            -  CaptureWidth (int, optional): Width of the captured
               image. Defaults to 256.
            -  CaptureHeight (int, optional): Height of the captured
               image. Defaults to 256.
            -  IsOutput (bool, optional): By default ``set_camera``
               function only sets the internal state. ``set_camera``
               only provides log output and not the actual captured
               image. ``set_control`` when called it returns the actual
               output. IF you want to force ``set_camera`` to return the
               image, set ``IsOutput`` to True. Defaults to False.

            **Capture Types**

            One of the main challenges in Machine Learning is the task
            of getting large amounts of training data in the right
            format. Deep learning, and machine learning more generally,
            needs huge training sets to work properly. Virtual worlds
            can provide a wealth of training data. However, it must
            consist of more than just the final image: object
            categorization, optical flow, etc

            ``Capture Types`` can be set to the following:

            ============ =====================
            =============================================================================================================================
            Capture Type Type                  Details
            ============ =====================
            =============================================================================================================================
            0            Scene Capture         Capture from scene Camera
            1            Instance Segmentation Each object in the scene gets unique color
            2            Semantic segmentation Objects are assigned color based on their category
            3            Depth                 Pixels are colored according to their motion in the relation to the camera
            4            Normals               Surfaces are colored according to their orientation in relation to the camera
            5            Optical Flow          Pixels are colored according to their distance from the camera (Only visible when Airplane or Object in reference are moving)
            ============ =====================
            =============================================================================================================================

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            .. rubric:: Implementation
               details\ `¶ <#Implementation-details>`__
               :name: Implementation-details

            First of all
            ``AC_Airplane_CameraController.OnSceneChange()`` calls
            ColorEncoding class to encode unique object idenitifier and
            layer as RGB color. These colors are stored in
            MaterialPropertyBlock for each object and are automatically
            passed into the shaders when rendering.

            Upon start ``AC_Airplane_CameraController`` component
            creates hidden camera for every single pass of output data
            (image segmentation, optical flow, depth, etc). These
            cameras allow to override usual rendering of the scene and
            instead use custom shaders to generate the output. These
            cameras are attached to different scene camera using
            ``Camera.targetDisplay property`` - handy for preview in the
            Editor.

            For Image segmentation and Object categorization pass
            special replacement shader is set with
            \`Camera.SetReplacementShader(). It overrides shaders that
            would be otherwise used for rendering and instead outputs
            encoded object id or layer.

            Optical flow and Depth pass cameras request additional data
            to be rendered with ``DepthTextureMode.Depth`` and
            ``DepthTextureMode.MotionVectors`` flags. Rendering of these
            cameras is followed by drawing full screen quad
            ``CommandBuffer.Blit()`` with custom shaders that convert
            24/16bit-per-channel data into the 8-bit RGB encoding.

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            .. rubric:: Importing
               Requirements\ `¶ <#Importing-Requirements>`__
               :name: Importing-Requirements

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [1]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     import sys
                     sys.path.append("../../")
                     from pprint import pprint
                     import PIL.Image as Image
                     import base64
                     import numpy as np
                     from io import BytesIO
                     from matplotlib.pyplot import  imshow
                     import matplotlib.pyplot as plt

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [2]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     from Python.src.airctrl import environment 
                     from Python.src.airctrl import sample_generator
                     from Python.src.airctrl.utils import unity
                     A =  environment.Trigger()
                     L = unity.Launch()
                     port = 7858
                     process = L.launch_executable("/home/supatel/Games/AirControl_2021/Build/1.3.0/Linux/v1.3.0-AirControl.x86_64", server_port=port)

   .. container:: jp-Cell-outputWrapper

      .. container::
      jp-Collapser jp-OutputCollapser jp-Cell-outputCollapser

      .. container:: jp-OutputArea jp-Cell-outputArea

         .. container:: jp-OutputArea-child

            .. container:: jp-OutputPrompt jp-OutputArea-prompt

            .. container:: jp-RenderedText jp-OutputArea-output

               ::

                  Now call method `.get_connected(port=<Default 8053>)` to get connected

                  Loading environment from /home/supatel/Games/AirControl_2021/Build/1.3.0/Linux/v1.3.0-AirControl.x86_64 at port 7858 client ip 127.0.1.1 client port 7858

                  Sleeping for 5 seconds to allow environment load

                  [UnityMemory] Configuration Parameters - Can be set up in boot.config
                      "memorysetup-bucket-allocator-granularity=16"
                      "memorysetup-bucket-allocator-bucket-count=8"
                      "memorysetup-bucket-allocator-block-size=4194304"
                      "memorysetup-bucket-allocator-block-count=1"
                      "memorysetup-main-allocator-block-size=16777216"
                      "memorysetup-thread-allocator-block-size=16777216"
                      "memorysetup-gfx-main-allocator-block-size=16777216"
                      "memorysetup-gfx-thread-allocator-block-size=16777216"
                      "memorysetup-cache-allocator-block-size=4194304"
                      "memorysetup-typetree-allocator-block-size=2097152"
                      "memorysetup-profiler-bucket-allocator-granularity=16"
                      "memorysetup-profiler-bucket-allocator-bucket-count=8"
                      "memorysetup-profiler-bucket-allocator-block-size=4194304"
                      "memorysetup-profiler-bucket-allocator-block-count=1"
                      "memorysetup-profiler-allocator-block-size=16777216"
                      "memorysetup-profiler-editor-allocator-block-size=1048576"
                      "memorysetup-temp-allocator-size-main=4194304"
                      "memorysetup-job-temp-allocator-block-size=2097152"
                      "memorysetup-job-temp-allocator-block-size-background=1048576"
                      "memorysetup-job-temp-allocator-reduction-small-platforms=262144"
                      "memorysetup-temp-allocator-size-background-worker=32768"
                      "memorysetup-temp-allocator-size-job-worker=262144"
                      "memorysetup-temp-allocator-size-preload-manager=262144"
                      "memorysetup-temp-allocator-size-nav-mesh-worker=65536"
                      "memorysetup-temp-allocator-size-audio-worker=65536"
                      "memorysetup-temp-allocator-size-cloud-worker=32768"
                      "memorysetup-temp-allocator-size-gfx=262144"

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [3]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     # get connected to server
                     A.get_connected(port=port)

   .. container:: jp-Cell-outputWrapper

      .. container::
      jp-Collapser jp-OutputCollapser jp-Cell-outputCollapser

      .. container:: jp-OutputArea jp-Cell-outputArea

         .. container:: jp-OutputArea-child

            .. container:: jp-OutputPrompt jp-OutputArea-prompt

            .. container:: jp-RenderedText jp-OutputArea-output

               ::

                  Connecting with port 7858

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            .. rubric:: Examples (Cockpit
               Camera)\ `¶ <#Examples-(Cockpit-Camera)>`__
               :name: Examples-(Cockpit-Camera)

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Scene Capture**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [6]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=1,CaptureType=0,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

   .. container:: jp-Cell-outputWrapper

      .. container::
      jp-Collapser jp-OutputCollapser jp-Cell-outputCollapser

      .. container:: jp-OutputArea jp-Cell-outputArea

         .. container:: jp-OutputArea-child

            .. container:: jp-OutputPrompt jp-OutputArea-prompt

            .. container:: jp-RenderedText jp-OutputArea-output

               ::

                  CPU times: user 44.6 s, sys: 7.02 s, total: 51.6 s
                  Wall time: 51.6 s

         .. container:: jp-OutputArea-child

            .. container:: jp-OutputPrompt jp-OutputArea-prompt

            .. container:: jp-RenderedImage jp-OutputArea-output

               |image0|

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Instance Segmentation**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=0,CaptureType=1,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Semantic segmentation**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=0,CaptureType=2,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Depth**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=0,CaptureType=3,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Normals**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=0,CaptureType=4,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Optical Flow**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=0,CaptureType=5,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            .. rubric:: Example (External
               Camera)\ `¶ <#Example-(External-Camera)>`__
               :name: Example-(External-Camera)

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Scene Capture**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=1,CaptureType=0,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Instance Segmentation**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=1,CaptureType=1,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Semantic segmentation**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=1,CaptureType=2,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Depth**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=1,CaptureType=3,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Normals**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=1,CaptureType=4,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            **Optical Flow**

.. container:: jp-Cell jp-CodeCell jp-Notebook-cell jp-mod-noOutputs

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

            In [ ]:

         .. container::
         jp-CodeMirrorEditor jp-Editor jp-InputArea-editor

            .. container:: CodeMirror cm-s-jupyter

               .. container:: highlight hl-ipython3

                  ::

                     ## no movement no optcal flow
                     output = A.set_camera(ActiveCamera=1, IsCapture=True,CaptureCamera=1,CaptureType=5,CaptureHeight=256,CaptureWidth=256,IsOutput=True)
                     image = output['ScreenCapture']
                     if image != "":
                         im = Image.open(BytesIO(base64.b64decode(image)))
                         imshow(np.asarray(im))
                         plt.axis('off')

.. container:: jp-Cell jp-MarkdownCell jp-Notebook-cell

   .. container:: jp-Cell-inputWrapper

      .. container::
      jp-Collapser jp-InputCollapser jp-Cell-inputCollapser

      .. container:: jp-InputArea jp-Cell-inputArea

         .. container:: jp-InputPrompt jp-InputArea-prompt

         .. container::
         jp-RenderedHTMLCommon jp-RenderedMarkdown jp-MarkdownOutput

            .. rubric:: Reference\ `¶ <#Reference>`__
               :name: Reference

            #. `Replacement
               Shaders <https://docs.unity3d.com/Manual/SL-ShaderReplacement.html>`__
            #. `Command
               Buffers <https://docs.unity3d.com/Manual/GraphicsCommandBuffers.html>`__
            #. `Depth and Motion
               Vectors <https://docs.unity3d.com/Manual/SL-CameraDepthTexture.html>`__
            #. `MaterialPropertyBlock <https://docs.unity3d.com/ScriptReference/MaterialPropertyBlock.html>`__

.. |image0| image:: data:image/png;base64,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%0A
   :class: jp-needs-light-background

