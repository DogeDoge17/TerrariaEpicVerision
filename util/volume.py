from pydub import AudioSegment
import math
import os

def find_files_recursively(directory, extension=None):
    found_files = []
    for root, dirs, files in os.walk(directory):
        for file in files:
            if extension is None or file.endswith(extension):
                found_files.append(os.path.join(root, file))
    return found_files

def run_as_mp3(input_file, output_file):
    if os.path.isfile(output_file):
        os.remove(output_file)
    os.system(f"ffmpeg -i \"{input_file}\" \"{output_file}\"")
    if "neco" in input_file.lower():
        match_volume(output_file, ref_file, 5)
    else:
        match_volume(output_file, ref_file)
    os.remove(input_file)
    os.system(f"ffmpeg -i \"{output_file}\" \"{input_file}\"")

def match_volume(target_file, reference_file, arg = 0):
    # Load audio files
    target_audio = AudioSegment.from_file(target_file)
    reference_audio = AudioSegment.from_file(reference_file)
    
    refVol = reference_audio.dBFS
    
    if arg > 0:
        refVol += arg
    elif arg < 0:
        refVol -= arg
    
    # Calculate the volume difference in dB
    volume_difference = refVol - target_audio.dBFS
    
    # Adjust volume of target audio
    adjusted_target_audio = target_audio + volume_difference
    
    # Export the adjusted audio to a new file
    adjusted_target_audio.export(target_file, format="wav")         

# Example usage:
ref_file = "Female_Hit_0.wav"

inputs = find_files_recursively("..\\Sounds", ".ogg")
inputs += find_files_recursively("..\\Sounds", ".mp3")
inputs += find_files_recursively("..\\Sounds", ".wav")

for input_file in inputs:
    run_as_mp3(input_file, "temp.wav")
    #match_volume(input_file, ref_file)
    print("Finished processing", input_file)
    
#match_volume(input_file, ref_file, output_file)
#normalize_volume(input_file, output_file, reference_dB, target_dB)
